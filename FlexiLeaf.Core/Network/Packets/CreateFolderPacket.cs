using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Core.Network.Packets
{
    public class CreateFolderPacket : Packet
    {
        public CreateFolderPacket() { }
        public CreateFolderPacket(string path, string name)
        {
            this.Path = path;
            this.Name = name;
        }
        public override int MessageId => Id;

        public static int Id => 23;

        public string Path { get; set; }
        public string Name { get; set; }

    }
}
