using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Server
{
    internal class ServerConnectionHandler
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private ImageList _imageList;
        private Dictionary<string, ImageBlock> _sentBlocks = new Dictionary<string, ImageBlock>();

        public ServerConnectionHandler(TcpClient client, Dictionary<string, ImageBlock> sentBlocks)
        {
            _client = client;
            _stream = _client.GetStream();
            _imageList = new ImageList();
            _imageList.CreateImageList();
            _sentBlocks = sentBlocks;
        }

        public async Task HandleClientConnection()
        {
            try
            {
                //receiving message header from proxy
                byte[] requestType = new byte[4];
                await _stream.ReadAsync(requestType, 0, requestType.Length);
                int requestTypeInt = BitConverter.ToInt32(requestType, 0);
                Console.WriteLine("header received from client: " + requestTypeInt);

                //receiving message from proxy
                byte[] messageBytes = new byte[_client.ReceiveBufferSize - requestType.Length];
                int bytesRead = await _stream.ReadAsync(messageBytes, 0, messageBytes.Length);
                string message = Encoding.UTF8.GetString(messageBytes, 0, bytesRead);
                Console.WriteLine("Received message: {0}", message);

                //check if the message is a request for an image or a request for a list of images

                if (requestTypeInt == 1)
                {
                    await SendListOfImageNames();

                }
                else if (requestTypeInt == 2)
                {
                    await SendImage(message);
                }
                else if (requestTypeInt == 4)
                {
                    _sentBlocks.Clear();
                }
                else if (requestTypeInt == 5)
                {
                    Thread formThread = new Thread(OpenServerGUI);
                    formThread.SetApartmentState(ApartmentState.STA);
                    formThread.Start();
                    formThread.Join();
                    
                }
                else
                {
                    byte[] response = Encoding.ASCII.GetBytes("You did not ask for an image. Go away");
                    await _stream.WriteAsync(response, 0, response.Length);
                    Console.WriteLine("sent {0} bytes to client because they did not ask for an image.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred at the server end {0}", ex.Message);
            }
            finally
            {
                Console.WriteLine("Server job done. Goodbye");
                _stream.Close();
                _client.Close();
            }
        }

        private async Task SendListOfImageNames()
        {
            List<string> listOfImageNames = new List<string>();

            for (int i = 0; i < _imageList.getImageList().Count; i++)
            {
                listOfImageNames.Add(_imageList.getImageList().ElementAt(i).Key);

            }
            string imageListString = string.Join(",", listOfImageNames);
            byte[] imageNamesBytes = Encoding.ASCII.GetBytes(imageListString);
            byte[] lengthOfStringBytes = BitConverter.GetBytes(imageNamesBytes.Length);
            await _stream.WriteAsync(lengthOfStringBytes, 0, lengthOfStringBytes.Length);
            await _stream.WriteAsync(imageNamesBytes, 0, imageNamesBytes.Length);
        }

        private async Task SendImage(string message)
        {
            string filePath = "";
            if (_imageList.ContainsKey(message))
            {
                filePath = _imageList.GetImagePath(message);
            }            

            byte[] imageBytes = File.ReadAllBytes(filePath);
            
            int targetBlockSize = 2047;
            uint mask = 0b11111111111111111111000000000000;
            ImageFragmenter imageFragmenter = new ImageFragmenter(imageBytes, targetBlockSize, mask, _sentBlocks);
            Dictionary<string, ImageBlock> requestedImageBlocks = imageFragmenter.FragmentImageIntoBlocks();
            await SendRequestedBlocks(requestedImageBlocks);            
            Console.WriteLine("finished send image method");
        }

        private async Task SendRequestedBlocks(Dictionary<string, ImageBlock> requestedBlocks)
        {
            try
            {
                byte[] blockCount = BitConverter.GetBytes(requestedBlocks.Count);
                await _stream.WriteAsync(blockCount, 0, blockCount.Length);
                int processedBlockCount = 0;

                foreach (KeyValuePair<string, ImageBlock> blockEntry in requestedBlocks)
                {
                    string requestedBlockHash = blockEntry.Key;
                    ImageBlock requestedBlock = blockEntry.Value;

                    if (_sentBlocks.ContainsKey(requestedBlockHash))
                    {
                        await _stream.WriteAsync(BitConverter.GetBytes(true), 0, 1);
                        byte[] blockHashBytes = Encoding.UTF8.GetBytes(requestedBlockHash);
                        await _stream.WriteAsync(blockHashBytes, 0, blockHashBytes.Length);
                    }
                    else
                    {
                        byte[] hashFlagBytes = BitConverter.GetBytes(false);
                        await _stream.WriteAsync(hashFlagBytes, 0, hashFlagBytes.Length);

                        byte[] blockHashBytes = Encoding.ASCII.GetBytes(requestedBlockHash);
                        await _stream.WriteAsync(blockHashBytes, 0, blockHashBytes.Length);

                        byte[] serializedBlockData = SerializeImageBlock(requestedBlock);
                        byte[] blockLength = BitConverter.GetBytes(serializedBlockData.Length);
                        await _stream.WriteAsync(blockLength, 0, blockLength.Length);
                        await _stream.WriteAsync(serializedBlockData, 0, serializedBlockData.Length);

                        _sentBlocks.Add(requestedBlockHash, requestedBlock);
                        processedBlockCount++;
                        
                    }
                }
                Console.WriteLine("sent blocks: " + processedBlockCount + " out of: " + requestedBlocks.Count);
                Console.WriteLine("finished sending blocks");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"error sending the image blocks from the server : {ex.Message}");
            }
        }

        public byte[] SerializeImageBlock(ImageBlock block)
        {
            if(block == null)
            {
                return null;
            }
            string blockJsonString = JsonSerializer.Serialize(block);
            byte[] serializedBlockData = Encoding.UTF8.GetBytes(blockJsonString);
            return serializedBlockData;
        }

        private void OpenServerGUI()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerGUI(_imageList.GetImageDirectory()));
        }
    }
}
