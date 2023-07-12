using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.ControlHub.Handlers
{
    internal class CommandHandlers
    {
        [PacketHandler]
        public static void GetCommandResponce(CommandPacket packet, TcpClient client)
        {
            Form1.Instance.AddCommandDataText(packet.Command);
        }

    }
}
