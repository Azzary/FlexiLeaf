using FlexiLeaf.Core.Network.Packets;
using System.Reflection;
using FlexiLeaf.Core.Network;

namespace FlexiLeaf.ControlHub
{
    internal static class Program
    {

        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PacketHandler.Init(Assembly.GetExecutingAssembly(), new Type[] { typeof(Packet), typeof(TcpClient) });

            var client = TcpClient.Instance;
            Application.Run(new Form1());
        }
        
    }
}
