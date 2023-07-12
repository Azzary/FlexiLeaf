using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Core.Network.Packets
{
    public class UpdateProcessPacket : Packet
    {
        public UpdateProcessPacket() { }
        public UpdateProcessPacket(List<ProcessPacket> process) 
        {
            Process = process;
        }
        public override int MessageId => Id;

        public static int Id => 10;

        public List<ProcessPacket> Process { get; set; }

    }
}
