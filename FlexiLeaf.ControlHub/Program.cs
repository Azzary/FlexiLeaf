using System.IO;
using Newtonsoft.Json;
using FlexiLeaf.Core.Network.Packets;
using System.Reflection;
using FlexiLeaf.Core.Network;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace FlexiLeaf.ControlHub
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppSettings.Instance.LoadConfiguration();
            PacketHandler.Init(Assembly.GetExecutingAssembly(), new Type[] { typeof(Packet), typeof(TcpClient) });
            var client = TcpClient.Instance;
            Application.Run(new Form1());
        }


    }
}
