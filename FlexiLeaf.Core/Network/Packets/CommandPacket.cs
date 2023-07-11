namespace FlexiLeaf.Core.Network.Packets
{
    public class CommandPacket : Packet
    {
        public CommandPacket() { }
        public CommandPacket(string Command)
        {
            this.Command = Command;
        }
        public override int MessageId => Id;

        public static int Id => 9;

        public string Command { get; set; }

    }
}
