using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Core.Network.Packets
{
    public class KillProcessPacket : Packet
    {
        public KillProcessPacket() { }
        public KillProcessPacket(ProcessPacket ProcessPacket)
        {
            this.ProcessPacket = ProcessPacket;
        }
        public override int MessageId => Id;

        public static int Id => 15;

        public ProcessPacket ProcessPacket { get; set; }

    }
}
