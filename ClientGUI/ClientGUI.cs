using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Reflection;

namespace ClientGUI
{
    public partial class ClientGUI : Form
    {
        IPAddress _serverAddress = IPAddress.Parse("127.0.0.1");

        public ClientGUI()
        {
            InitializeComponent();
        }

        private async void getSelectedImage_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                NetworkStream stream = await ConnectAsync(client);

                //define a request type
                SendRequestType(2, stream);

                //specify the image name
                if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select an image from the list");
                    return;
                }

                string selectedImageName = listBox1.SelectedItem.ToString();
                SendMessage(selectedImageName, stream);

                //receive image from server                
                byte[] imageBytes = await ReceiveData(stream);

                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    Bitmap image = new Bitmap(memoryStream);
                    pictureBox1.Image = image;
                    SaveImage(image);
                }
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void getImageList_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                TcpClient client = new TcpClient();
                NetworkStream stream = await ConnectAsync(client);

                //send request type
                SendRequestType(1, stream);

                //send message body                          
                SendMessage("getImageList", stream);

                //receive list of names
                byte[] imageNamesBytes = await ReceiveData(stream);
                string receivedNames = Encoding.ASCII.GetString(imageNamesBytes);
                string[] imageNameList = receivedNames.Split(',');
                foreach (string name in imageNameList)
                {
                    listBox1.Items.Add(name);
                }
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void viewCacheButton_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                NetworkStream stream = await ConnectAsync(client);

                //send a request type
                SendRequestType(3, stream);

                //send message body
                SendMessage("viewCache", stream);
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<NetworkStream> ConnectAsync(TcpClient client)
        {
            await client.ConnectAsync(_serverAddress, 8081);
            NetworkStream stream = client.GetStream();
            return stream;
        }

        private async void SendRequestType(int requestType, NetworkStream stream)
        {
            byte[] requestTypeBytes = BitConverter.GetBytes(requestType);
            await stream.WriteAsync(requestTypeBytes, 0, requestTypeBytes.Length);
        }

        private async void SendMessage(string message, NetworkStream stream)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
        }

        private async Task<byte[]> ReceiveData(NetworkStream stream)
        {
            //receive data from server
            byte[] lengthBytes = new byte[4];
            await stream.ReadAsync(lengthBytes, 0, lengthBytes.Length);
            int dataLength = BitConverter.ToInt32(lengthBytes, 0);

            byte[] dataBytes = new byte[dataLength];
            int totalBytesReceived = 0;
            while (totalBytesReceived < dataLength)
            {
                int bytesReceived = await stream.ReadAsync(dataBytes, totalBytesReceived, dataLength - totalBytesReceived);
                totalBytesReceived += bytesReceived;
            }
            return dataBytes;
        }

        public void SaveImage(Bitmap image)
        {
            //saving image
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyPath = assembly.Location;
            string currentDirectory = Path.GetDirectoryName(assemblyPath);

            for (int i = 0; i < 4; i++)
            {
                currentDirectory = Path.GetDirectoryName(currentDirectory);
                if (currentDirectory == null)
                {
                    Console.WriteLine("Cannot go up any further!");
                    break;
                }
            }

            DirectoryInfo imageDirectory = new DirectoryInfo(currentDirectory + "/images");
            ImageFormat imageFormat = ImageFormat.Jpeg;
            string imageNameTest = listBox1.SelectedItem.ToString();
            string fullPath = Path.Combine(imageDirectory.FullName, imageNameTest);
            Directory.CreateDirectory(imageDirectory.FullName);
            image.Save(fullPath, imageFormat);

            MessageBox.Show("Image saved to " + fullPath);
        }

        private async void openServerGUI_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                NetworkStream stream = await ConnectAsync(client);

                //send a request type
                SendRequestType(5, stream);

                //send message body
                SendMessage("viewServerGUI", stream);
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}