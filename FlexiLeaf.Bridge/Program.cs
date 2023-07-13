using FlexiLeaf.Bridge;
using FlexiLeaf.ControlHub.Network;
using FlexiLeaf.Core.Network.Packets;
using System.Reflection;


namespace FlexiLeaf.ControlHub
{
    internal static class Program
    {
        static async Task Main()
        {
            AppSettings.Instance.LoadConfiguration();
            PacketHandler.Init(Assembly.GetExecutingAssembly(), new Type[] { typeof(Packet), typeof(Client) });
            var server1 = new TcpServer();
            var server2 = new TcpServer();

            var serverTask1 = server1.Start(AppSettings.Instance.IPAddress, AppSettings.Instance.PortStealthRunner, false);
            var serverTask2 = server2.Start(AppSettings.Instance.IPAddress, AppSettings.Instance.PortControlHub, true);
            await Task.WhenAll(serverTask1, serverTask2);

        }
    }
}