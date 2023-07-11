using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Core.Network.Packets
{
    public class UpdateClientListPacket : Packet
    {
        public UpdateClientListPacket() { }
        public UpdateClientListPacket(List<string> clients) 
        { 
            Clients = clients;
        }
        public override int MessageId => Id;

        public static int Id => 4;

        public List<string> Clients { get; set; }

    }
}
