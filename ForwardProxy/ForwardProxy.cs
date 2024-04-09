namespace ForwardProxy
{
    internal class ForwardProxy
    {
        static async Task Main(string[] args)
        {
            ProxyListener listener = new ProxyListener();
            await listener.StartListening();

        }
    }
}