using FlexiLeaf.ControlHub.Network;
using FlexiLeaf.Core.Network.Packets;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;




namespace FlexiLeaf.ControlHub
{
    internal static class Program
    {
        static async Task Main()
        {
            PacketHandler.Init(Assembly.GetExecutingAssembly(), new Type[] { typeof(Packet), typeof(Client) });
            var ipAddress = "127.0.0.1";
            var port = 27856;
            var server1 = new TcpServer();
            var server2 = new TcpServer();

            var serverTask1 = server1.Start(ipAddress, port, false);
            var serverTask2 = server2.Start(ipAddress, port + 1, true);
            await Task.WhenAll(serverTask1, serverTask2);

        }
    }
}