using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlexiLeaf.Core.Operations.MouseOperations;

namespace FlexiLeaf.Core.Network.Packets
{
    public class LoginPacket : Packet
    {
        public LoginPacket() { }
        public LoginPacket(string password)
        {
            Password = password;
        }
        public override int MessageId => Id;

        public static int Id => -88;

        public string Password { get; set; }

    }
}
