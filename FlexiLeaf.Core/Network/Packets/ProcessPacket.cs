using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Core.Network.Packets
{
    public class ProcessPacket : Packet
    {
        public ProcessPacket() { }
        public ProcessPacket(string name, int PID, string CPU, string RAM, string Network)
        {
            Name = name;
            this.CPU = CPU; 
            this.RAM = RAM; 
            this.PID = PID;
            this.Network = Network;
        }
        public override int MessageId => Id;

        public static int Id => 11;

        public string Name { get; set; }
        public string CPU { get; set; }
        public int PID { get; set; }
        public string RAM { get; set; }
        public string Network { get; set; }
    }
}