using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class ServerListener
    {
        private readonly IPAddress _serverAddress = IPAddress.Parse("127.0.0.1");
        private readonly int _port = 8082;
        private Dictionary<string, ImageBlock> _sentBlocks = new Dictionary<string, ImageBlock>();

        public ServerListener() 
        { 

        }

        public async Task StartListening()
        {
            TcpListener listener = new TcpListener(_serverAddress, _port);
            listener.Start();
            Console.WriteLine("waiting for a connection...");
            try
            {
                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Client connected from {0}", client.Client.RemoteEndPoint);

                    ServerConnectionHandler clientHandler = new ServerConnectionHandler(client, _sentBlocks);
                    await clientHandler.HandleClientConnection();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to listen {ex}");
            }
            
            
        }
    }
}
