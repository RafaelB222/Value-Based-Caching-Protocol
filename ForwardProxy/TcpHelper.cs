using System.Net.Sockets;

namespace ForwardProxy
{
    internal class TcpHelper
    {

        public TcpHelper() { 

        }

        public async Task<int> ReceiveMessageHeaderFromClient(NetworkStream clientStream)
        {
            byte[] requestType = new byte[4];
            await clientStream.ReadAsync(requestType, 0, requestType.Length);
            int requestTypeCode = BitConverter.ToInt32(requestType, 0);
            Console.WriteLine("header received from client: " + requestTypeCode);
            return requestTypeCode;
        }


        public async Task<byte[]> ReceiveMessageFromClientAsync(NetworkStream clientStream)
        {
            byte[] clientMessageBuffer = new byte[1024];
            int bytesRead = await clientStream.ReadAsync(clientMessageBuffer, 0, clientMessageBuffer.Length);
            return bytesRead > 0 ? clientMessageBuffer.Take(bytesRead).ToArray() : Array.Empty<byte>();
        }

        public async Task ForwardMessageToServer(int requestTypeCode, byte[] clientMessageBuffer, NetworkStream serverStream)
        {
            byte[] requestTypeBytes = BitConverter.GetBytes(requestTypeCode);
            await serverStream.WriteAsync(requestTypeBytes, 0, requestTypeBytes.Length);
            await serverStream.WriteAsync(clientMessageBuffer, 0, clientMessageBuffer.Length);
            Console.WriteLine("forwarded message to server");
        }
    }
}
