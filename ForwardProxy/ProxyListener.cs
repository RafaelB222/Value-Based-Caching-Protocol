using System.Net;
using System.Net.Sockets;

namespace ForwardProxy
{
    internal class ProxyListener
    {
        private readonly IPAddress _serverAddress = IPAddress.Parse("127.0.0.1");
        private readonly int _port = 8081;
        
        private ProxyServerLog _log;
        private Cache _cache;


        public ProxyListener()
        {
            
            _log = new ProxyServerLog();
            _cache = new Cache();
        }

        public async Task StartListening()
        {
            TcpListener listener = new TcpListener(_serverAddress, _port);
            listener.Start();
            Console.WriteLine("proxy waiting for a connection...");
            try
            {
                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Client connected from {0}", client.Client.RemoteEndPoint);

                    ProxyConnectionHandler clientHandler = new ProxyConnectionHandler(client, _log, _cache);
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
