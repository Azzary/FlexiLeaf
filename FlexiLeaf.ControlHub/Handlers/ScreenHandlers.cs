using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.ControlHub.Handlers
{
    internal class ScreenHandlers
    {
        [PacketHandler]
        public static void UpdateImage(ScreenPacket packet, TcpClient client)
        {
            if (packet.ShowScreen == true)
            {
                var bitmap = ScreenPacket.ByteArrayToBitmap(packet.ImageArray);
                Form1.Instance.ScreenTab.ChangeImage(bitmap);
            }
        }
    }
}
