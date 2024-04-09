namespace Server
{
    internal class Server
    {
        
        static async Task Main(string[] args)
        {            
            ServerListener listener = new ServerListener();
            await listener.StartListening();           
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }        
    }
}