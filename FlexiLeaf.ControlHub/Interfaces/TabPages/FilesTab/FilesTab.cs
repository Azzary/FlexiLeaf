using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.ControlHub.Interfaces.TabPages.FilesTab
{
    public class FilesTab : TabPage
    {
        private TextBox TargetFolderFile = new();
        private TreeView FileExplorer = new();
        private Button CreateNewFolder = new();
        private Button SendFiles = new();


        public FilesTab() 
        {
            SuspendLayout();
            Location = new Point(4, 24);
            Name = "Files";
            Padding = new Padding(3);
            Size = new Size(767, 390);
            TabIndex = 1;
            Text = "Files";
            Controls.Add(CreateNewFolder);
            Controls.Add(FileExplorer);
            Controls.Add(TargetFolderFile);
            Controls.Add(SendFiles);
            UseVisualStyleBackColor = true;
            ResumeLayout(false);
            PerformLayout();
            this.FileExplorer.AfterExpand += new TreeViewEventHandler(this.FileExplorer_AfterCollapse);
            Init();
        }

        public void Init()
        {
            // 
            // CreateNewFolder
            // 
            CreateNewFolder.Location = new Point(380, 17);
            CreateNewFolder.Name = "CreateNewFolder";
            CreateNewFolder.Size = new Size(132, 23);
            CreateNewFolder.TabIndex = 7;
            CreateNewFolder.Text = "Create New Folder";
            CreateNewFolder.UseVisualStyleBackColor = true;
            CreateNewFolder.Click += CreateNewFolder_Click;
            // 
            // FileExplorer
            // 
            FileExplorer.Location = new Point(6, 46);
            FileExplorer.Name = "FileExplorer";
            FileExplorer.Size = new Size(755, 338);
            FileExplorer.TabIndex = 6;
            FileExplorer.NodeMouseClick += FileExplorer_NodeMouseClick;
            // 
            // TargetFolderFile
            // 
            TargetFolderFile.Location = new Point(116, 17);
            TargetFolderFile.Name = "TargetFolderFile";
            TargetFolderFile.Size = new Size(258, 23);
            TargetFolderFile.TabIndex = 5;
            // 
            // SendFiles
            // 
            SendFiles.Location = new Point(11, 17);
            SendFiles.Name = "SendFiles";
            SendFiles.Size = new Size(99, 23);
            SendFiles.TabIndex = 4;
            SendFiles.Text = "Send Files";
            SendFiles.UseVisualStyleBackColor = true;
            SendFiles.Click += SendFiles_Click;
        }

        public bool FileExplorerEmpty => FileExplorer.Nodes.Count == 0;

        public void UpdateFileExplorer(FileExplorerPacket packet)
        {
            Invoke(new Action(() =>
            {
                updateFileExplorer(packet);
            }));
        }

        // ca c'est pas mal null quand meme...
        private async void FileExplorer_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.IsExpanded)
            {
                var nodePath = ((string)e.Node.Tag).Replace("TreeNode: ", "");
                await TcpClient.Instance.Send(new FileExplorerPacket(nodePath));
            }
        }

        private async void FileExplorer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                var nodePath = ((string)e.Node.Tag).Replace("TreeNode: ", "");
                await TcpClient.Instance.Send(new FileExplorerPacket(nodePath));
            }
            TargetFolderFile.Text = ((string)e.Node.Tag).Replace("TreeNode: ", "");
        }

        private async void CreateNewFolder_Click(object sender, EventArgs e)
        {
            using var dialog = new InputBox("New Folder Name");
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                string folderName = dialog.InputText;
                await TcpClient.Instance.Send(new CreateFolderPacket(TargetFolderFile.Text, folderName));
            }
        }

        private void SendFiles_Click(object sender, EventArgs e)
        {
            Thread newThread = new(new ThreadStart(OpenFileDialog));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();

        }


        private void updateFileExplorer(FileExplorerPacket packet)
        {
            var parentNode = FindTreeNodeByPath(packet.CurrentPath, FileExplorer.Nodes);

            if (parentNode == null)
            {
                foreach (var file in packet.Files)
                {
                    var node = new TreeNode(file.Name);
                    if (file.IsFolder)
                    {
                        node.NodeFont = new Font(FileExplorer.Font, FontStyle.Bold);
                    }
                    node.Tag = file.Name;
                    FileExplorer.Nodes.Add(node);
                }
            }
            else
            {
                parentNode.Nodes.Clear();
                packet.SortFiles();
                foreach (var file in packet.Files)
                {
                    var node = new TreeNode(file.Name);
                    if (file.IsFolder)
                    {
                        node.NodeFont = new Font(FileExplorer.Font, FontStyle.Bold);
                        node.Tag = packet.CurrentPath + file.Name + @"\";
                    }
                    else
                    {
                        node.Tag = packet.CurrentPath + file.Name;
                    }
                    parentNode.Nodes.Add(node);
                }
                parentNode.Expand();
            }
        }

        private TreeNode FindTreeNodeByPath(string path, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (((string)node.Tag).Replace("TreeNode: ", "") == path)
                {
                    return node;
                }

                TreeNode foundNode = FindTreeNodeByPath(path, node.Nodes);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }

        private async void OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new()
            {
                Multiselect = true
            };

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                Console.WriteLine("Send: " + selectedFiles.Length + " files");
                foreach (string file in selectedFiles)
                {
                    SendFilePacket filePacket = new(file)
                    {
                        TargetDirectory = TargetFolderFile.Text
                    };
                    while (!filePacket.ReadFileInChunks())
                    {
                        await TcpClient.Instance.Send(filePacket);
                        await Task.Delay(10);
                    }
                    await TcpClient.Instance.Send(filePacket);
                }
            }
        }

    }
}
