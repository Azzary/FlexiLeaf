using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlexiLeaf.Core.Operations.MouseOperations;

namespace FlexiLeaf.Core.Network.Packets
{
    public class MousePacket : Packet
    {
        public MousePacket() { }
        public MousePacket(int x, int y, MouseEventFlags mouseEvent)
        {
            X = x; Y = y;
            MouseEvent = mouseEvent;
        }
        public override int MessageId => Id;

        public static int Id => 2;

        public int X { get; set; }
        public int Y { get; set; }
        public MouseEventFlags MouseEvent { get; set; }

    }
}
