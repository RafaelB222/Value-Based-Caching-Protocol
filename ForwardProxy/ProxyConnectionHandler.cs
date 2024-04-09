using System.Net.Sockets;
using System.Text;
using Server;

namespace ForwardProxy
{
    internal class ProxyConnectionHandler
    {
        private TcpClient _client;
        private TcpClient _serverConnection;
        private NetworkStream _clientStream;
        private NetworkStream _serverStream;        
        private string _message;
        private ProxyServerLog _log;
        private Cache _cache;
        private TcpHelper tcpHelper;
        

        public ProxyConnectionHandler(TcpClient client, ProxyServerLog log, Cache cache)
        {
            _client = client;
            _serverConnection = new TcpClient("127.0.0.1", 8082);
            _clientStream = _client.GetStream();
            _serverStream = _serverConnection.GetStream();            
            _message = "";
            _log = log;
            _cache = cache;
            tcpHelper = new TcpHelper();
        }        

        public async Task HandleClientConnection()
        {
            try
            {                
                int requestTypeCode = await tcpHelper.ReceiveMessageHeaderFromClient(_clientStream);
                byte[] clientMessageBuffer = await tcpHelper.ReceiveMessageFromClientAsync(_clientStream);

                if (clientMessageBuffer.Length == 0)
                {
                    return;
                }

                _message = Encoding.UTF8.GetString(clientMessageBuffer);
                Console.WriteLine("Received message from client: {0}", _message);
                if (requestTypeCode == 1)
                {                    
                    await tcpHelper.ForwardMessageToServer(requestTypeCode, clientMessageBuffer, _serverStream);
                    await PassImageListFromServerToClient();
                }
                else if (requestTypeCode == 2)
                {                    
                    await tcpHelper.ForwardMessageToServer(requestTypeCode, clientMessageBuffer, _serverStream);
                    await ReceiveImageBlocksFromServer();
                }
                else if (requestTypeCode == 3)
                {
                    Thread formThread = new Thread(OpenProxyGUI);
                    formThread.SetApartmentState(ApartmentState.STA);
                    formThread.Start();
                    formThread.Join();
                }
                else if (requestTypeCode == 5)
                {
                    await tcpHelper.ForwardMessageToServer(requestTypeCode, clientMessageBuffer, _serverStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred at the server end {0}", ex.Message);
            }
            finally
            {
                Console.WriteLine("Proxy job done. Goodbye");
                _serverStream.Close();
                _serverConnection.Close();
                _clientStream.Close();
                _client.Close();
            }
        }       

        private async Task PassImageListFromServerToClient()
        {
            try
            {
                //Receiving image list from the server!!!
                byte[] lengthBytes = new byte[4];
                await _serverStream.ReadAsync(lengthBytes, 0, lengthBytes.Length);
                int imageListLength = BitConverter.ToInt32(lengthBytes, 0);

                byte[] imageListBytes = new byte[imageListLength];
                int totalBytesReceived = 0;
                while (totalBytesReceived < imageListLength)
                {
                    Console.WriteLine("receiving at least some bytes from the server");
                    int bytesReceived = await _serverStream.ReadAsync(imageListBytes, totalBytesReceived, imageListLength - totalBytesReceived);
                    totalBytesReceived += bytesReceived;
                }               

                //Sending image list to client...
                await _clientStream.WriteAsync(lengthBytes, 0, lengthBytes.Length);
                await _clientStream.WriteAsync(imageListBytes, 0, imageListBytes.Length);
                Console.WriteLine("image list sent from proxy to client.");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred at the server end {0}", ex.Message);
            }
        }        

        private async Task ReceiveImageBlocksFromServer()
        {
            try
            {
                ImageHelper imageHelper = new ImageHelper();
                Dictionary<string, ImageBlock> receivedImageBlocks = new Dictionary<string, ImageBlock>();

                //get the number of blocks
                byte[] blockCountBytes = new byte[4];
                await _serverStream.ReadAsync(blockCountBytes, 0, blockCountBytes.Length);
                int blockCount = BitConverter.ToInt32(blockCountBytes, 0);

                int totalBytesCount = 0;
                int cachedBytesUsed = 0;


                for(int i = 0; i < blockCount; i++)
                {
                    //receive the hash only flag
                    byte[] hashFlagBytes = new byte[1];
                    await _serverStream.ReadAsync(hashFlagBytes, 0, hashFlagBytes.Length);
                    bool isHashOnly = BitConverter.ToBoolean(hashFlagBytes,0);

                    //receive the block hash
                    byte[] hashBytes = new byte[32];
                    await _serverStream.ReadAsync(hashBytes, 0, hashBytes.Length);
                    string blockHash = Encoding.UTF8.GetString(hashBytes);

                    if(isHashOnly)
                    {
                        //if only the hash was sent, retrive the block from the cache and add to the list of received blocks
                        ImageBlock block = _cache.GetItemFromCache(blockHash);
                        receivedImageBlocks.Add(blockHash, block);
                        cachedBytesUsed += block.ImageData.Length;
                        totalBytesCount += block.ImageData.Length;
                    }
                    else
                    {
                        //if the whole block was sent, receive the hash, the block data, and add to the list of received blocks
                        byte[] blockLengthBytes = new byte[4];
                        await _serverStream.ReadAsync(blockLengthBytes, 0, blockLengthBytes.Length);
                        int blockLength = BitConverter.ToInt32(blockLengthBytes,0);

                        byte[] SerializedBlockBytes = new byte[blockLength];
                        await _serverStream.ReadAsync(SerializedBlockBytes, 0, SerializedBlockBytes.Length);

                        //create an image block to hold the new block data and store in the cache
                        ImageBlock newBlock = imageHelper.DeserializeImageBlock(SerializedBlockBytes);
                        _cache.AddItemToCache(blockHash, newBlock);
                        receivedImageBlocks.Add(blockHash, newBlock);
                        totalBytesCount += newBlock.ImageData.Length;
                    }
                }
                
                double percentOfCachedBytesUsed = (double)cachedBytesUsed / totalBytesCount * 100;
                Console.WriteLine("percent cached bytes used int = " +  percentOfCachedBytesUsed);

                //Reconstruct the image from the blocks and send to the client
                byte[] reconstructedImageData = imageHelper.ReconstructImageFromBlocks(receivedImageBlocks);
                byte[] imageLengthBytes = BitConverter.GetBytes(reconstructedImageData.Length);
                await _clientStream.WriteAsync(imageLengthBytes, 0, imageLengthBytes.Length);
                await _clientStream.WriteAsync(reconstructedImageData, 0, reconstructedImageData.Length);

                LogEntry newLogEntry = new LogEntry(_message, percentOfCachedBytesUsed);
                _log.AddLogEntry(newLogEntry);

                Console.WriteLine("Image sent from proxy to client");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving image blocks from the server: {ex.Message}");
            }
        }   
       
        private void OpenProxyGUI()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(_cache, _log, _serverStream));
        }        
    }
}
