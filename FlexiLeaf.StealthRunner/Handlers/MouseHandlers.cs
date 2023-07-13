using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using FlexiLeaf.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlexiLeaf.Core.Operations.MouseOperations;

namespace FlexiLeaf.StealthRunner.Handlers
{
    internal class MouseHandlers
    {
        [PacketHandler]
        public static void Click(MousePacket packet, TcpClient client)
        {
            MouseOperations.SetCursorPosition(packet.X, packet.Y);
            if (packet.MouseEvent != MouseEventFlags.Move)
            {
                MouseOperations.MouseEvent(packet.MouseEvent);
            }
        }

    }
}
