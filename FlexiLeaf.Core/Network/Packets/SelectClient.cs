using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Core.Network.Packets
{
    public class SelectClient : Packet
    {
        public SelectClient() { }
        public SelectClient(string client)
        {
            Client = client;
        }
        public override int MessageId => Id;

        public static int Id => 3;

        public string Client { get; set; }

    }
    
}
