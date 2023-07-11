using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Core.Network.Packets
{
    public abstract class Packet
    {
        public static int Id;

        public abstract int MessageId { get; }


    }
}
