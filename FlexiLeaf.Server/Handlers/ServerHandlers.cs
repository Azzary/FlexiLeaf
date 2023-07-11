using FlexiLeaf.Core.Network.Packets;
using FlexiLeaf.Core.Network;

namespace FlexiLeaf.ControlHub.Handlers
{
    internal static class ServerHandlers
    {
        public static List<string> Clients { get; private set; }

        [PacketHandler]
        public static void UpdateClientList(UpdateClientListPacket packet, TcpClient client)
        {
            Clients = packet.Clients;
            Form1.Instance.RefreshComboBox();
        }

    }
}
