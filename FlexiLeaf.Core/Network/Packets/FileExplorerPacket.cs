using System;
using System.Collections.Generic;
using System.IO;

namespace FlexiLeaf.Core.Network.Packets
{
    public class FileExplorerPacket : Packet
    {
        public FileExplorerPacket()
        {
            this.Files = new List<FilePacket>();
        }
        public FileExplorerPacket(string path)
            : this()
        {
            this.CurrentPath = path;
            ExplorePath();
        }
        public override int MessageId => Id;

        public static int Id => 20;

        public string CurrentPath { get; set; }

        public List<FilePacket> Files { get; set; }

        public void SortFiles()
        {
            this.Files = this.Files.OrderBy(f => !f.IsFolder).ThenBy(f => f.Name).ToList();
        }

        public void ExplorePath()
        {
            Files.Clear();
            // If the CurrentPath is empty or null, list all available drives
            if (string.IsNullOrEmpty(CurrentPath))
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    this.Files.Add(new FilePacket(drive.Name, isFolder: true));
                }
            }
            else
            {
                try
                {
                    if (!Directory.Exists(CurrentPath))
                    {
                        return;
                    }
                    foreach (var file in Directory.GetFiles(CurrentPath))
                    {
                        this.Files.Add(new FilePacket(Path.GetFileName(file)));
                    }
                    foreach (var directory in Directory.GetDirectories(CurrentPath))
                    {
                        this.Files.Add(new FilePacket(Path.GetFileName(directory), isFolder: true));
                    }
                }
                catch (Exception)
                {
                }
            }
        }

    }

    public class FilePacket : Packet
    {
        public FilePacket() { }

        public FilePacket(string Name, bool isFolder = false)
        {
            this.Name = Name;
            this.IsFolder = isFolder;
        }
        public override int MessageId => Id;

        public static int Id => 21;

        public string Name { get; set; }
        public bool IsFolder { get; set; } = false;

    }
}
