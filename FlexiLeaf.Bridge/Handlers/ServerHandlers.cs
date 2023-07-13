using FlexiLeaf.ControlHub.Network;
using FlexiLeaf.Core.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Bridge.Handlers
{
    internal class ServerHandlers
    {
        [PacketHandler]
        public async static void SelectClient(SelectClient packet, Client client)
        {
            TcpServer.TargetClient = TcpServer.clients.FirstOrDefault(x => x.ID == packet.Client);
        }


        [PacketHandler]
        public async static void login(LoginPacket packet, Client client)
        {
            if (packet.Password == AppSettings.Instance.Password)
            {
                if (TcpServer.MainClient != null)
                {
                    TcpServer.MainClient.OnDisconnect();
                }
                TcpServer.clients.Remove(client);
                TcpServer.MainClient = client;
                await TcpServer.MainClient.Send(new UpdateClientListPacket(TcpServer.clients.Select(client => client.ID).ToList()));
            }
        }

    }
}
