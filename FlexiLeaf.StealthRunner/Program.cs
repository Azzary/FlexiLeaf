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
            AppSettings.Instance.LoadConfiguration();
            await TcpClient.Instance.Connect(AppSettings.Instance.IPAddress, AppSettings.Instance.Port, "");
            while (true)
            {
                await Task.Delay(int.MaxValue);
            }
        }
    }
}
