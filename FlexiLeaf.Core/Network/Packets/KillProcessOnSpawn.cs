using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Core.Network.Packets
{
    public class KillProcessOnSpawn : Packet
    {
        public KillProcessOnSpawn() { }
        public KillProcessOnSpawn(List<string> KillProcess)
        {
            this.KillProcess = KillProcess;
        }
        public override int MessageId => Id;

        public static int Id => 17;

        public List<string> KillProcess { get; set; }

    }
}
