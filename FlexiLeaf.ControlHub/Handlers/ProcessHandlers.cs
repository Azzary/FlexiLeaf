using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using FlexiLeaf.Core.Operations;
using FlexiLeaf.Core.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexiLeaf.ControlHub.Handlers
{
    public static class ProcessHandlers
    {

        [PacketHandler]
        public static void GetProcessList(UpdateProcessPacket updatePacket, TcpClient client)
        {
            Form1.Instance.UpdateProcess(updatePacket);
        }

    }
}
