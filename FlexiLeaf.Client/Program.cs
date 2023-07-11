using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using System.Reflection;

namespace FlexiLeaf.StealthRunner
{
    class Program
    {
        static async Task Main()
        {
            PacketHandler.Init(Assembly.GetExecutingAssembly(), new Type[] { typeof(Packet), typeof(TcpClient) });
            var ipAddress = "127.0.0.1";
            var port = 27856;

            var client = TcpClient.Instance;

            await TcpClient.Instance.Connect(ipAddress, port, "");
            while (true)
            {
                Console.ReadKey();
            }
        }
    }
}
