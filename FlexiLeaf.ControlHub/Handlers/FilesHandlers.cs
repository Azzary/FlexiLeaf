using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.ControlHub.Handlers
{
    internal class FilesHandlers
    {

        [PacketHandler]
        public async static void FileExplorer(FileExplorerPacket packet, TcpClient Client)
        {
            Form1.Instance.UpdateFileExplorer(packet);
        }

        //[PacketHandler]
        //public static void HandleSendFilePacket(SendFilePacket packet, TcpClient Client)
        //{
        //    if (!Form1.Instance.tcs.Task.IsCompleted)
        //    {
        //        Form1.Instance.tcs.SetResult(true);
        //    }
        //}

    }
}
