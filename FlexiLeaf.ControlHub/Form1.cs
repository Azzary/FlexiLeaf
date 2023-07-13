using FlexiLeaf.ControlHub.Interfaces;
using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using FlexiLeaf.Core.Operations;
using Giny.Core.DesignPattern;



namespace FlexiLeaf.ControlHub
{


    public partial class Form1 : Form, ISingleton<Form1>
    {
        private static Form1? _instance;
        public static Form1 Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Form1();
                }
                return _instance;
            }
        }

        public Form1()
        {
            if (_instance != null)
            {
                return;
            }
            _instance = this;
            InitializeComponent();
            this.Load += Form1_Load;
            this.FileExplorer.AfterExpand += new TreeViewEventHandler(this.FileExplorer_AfterCollapse);
        }

        private async void FileExplorer_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            // Vérifie si le noeud a été refermé
            if (e.Node.IsExpanded)
            {
                var nodePath = ((string)e.Node.Tag).Replace("TreeNode: ", ""); // Suppose que le chemin d'accès est stocké dans le Tag du TreeNode
                await TcpClient.Instance.Send(new FileExplorerPacket(nodePath));
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await TcpClient.Instance.Connect(AppSettings.Instance.IPAddress, AppSettings.Instance.Port, AppSettings.Instance.Password);
        }

        private async void FileExplorer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                var nodePath = ((string)e.Node.Tag).Replace("TreeNode: ", ""); // Suppose que le chemin d'accès est stocké dans le Tag du TreeNode
                await TcpClient.Instance.Send(new FileExplorerPacket(nodePath));
            }
            TargetFolderFile.Text = ((string)e.Node.Tag).Replace("TreeNode: ", "");
        }


        public void UpdateFileExplorer(FileExplorerPacket packet)
        {
            Form1.Instance.Invoke(new Action(() =>
            {
                Form1.Instance._UpdateFileExplorer(packet);
            }));
        }
        private void _UpdateFileExplorer(FileExplorerPacket packet)
        {
            var parentNode = FindTreeNodeByPath(packet.CurrentPath, FileExplorer.Nodes);

            if (parentNode == null)
            {
                foreach (var file in packet.Files)
                {
                    // Create a new TreeNode
                    var node = new TreeNode(file.Name);

                    // If the file is actually a directory, make it bold
                    if (file.IsFolder)
                    {
                        node.NodeFont = new Font(FileExplorer.Font, FontStyle.Bold);
                    }
                    node.Tag = file.Name;
                    // Add the TreeNode to the TreeView
                    FileExplorer.Nodes.Add(node);
                }
            }
            else // parentNode is not null, so we add to it
            {
                parentNode.Nodes.Clear();
                packet.SortFiles();
                foreach (var file in packet.Files)
                {
                    // Create a new TreeNode
                    var node = new TreeNode(file.Name);

                    // If the file is actually a directory, make it bold
                    if (file.IsFolder)
                    {
                        node.NodeFont = new Font(FileExplorer.Font, FontStyle.Bold);
                        node.Tag = packet.CurrentPath + file.Name + @"\";
                    }
                    else
                    {
                        node.Tag = packet.CurrentPath + file.Name;
                    }
                    Console.WriteLine(node.Tag.ToString());
                    // Add the TreeNode to the parentNode
                    parentNode.Nodes.Add(node);
                }
                parentNode.Expand();
            }
        }

        private TreeNode FindTreeNodeByPath(string path, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (((string)node.Tag).Replace("TreeNode: ", "") == path) // Supposons que le chemin d'accès est stocké dans le Tag du TreeNode
                {
                    return node;
                }

                TreeNode foundNode = FindTreeNodeByPath(path, node.Nodes);
                if (foundNode != null) // Noeud trouvé dans les sous-noeuds
                {
                    return foundNode;
                }
            }

            return null; // Aucun noeud correspondant trouvé
        }



        public void RefreshComboBox()
        {
            Form1.Instance.Invoke(new Action(Form1.Instance.refreshComboBox));
        }
        private void refreshComboBox()
        {
            comboBoxClients.DataSource = null;
            comboBoxClients.DataSource = Handlers.ServerHandlers.Clients;
        }

        private async void comboBoxClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowScreen.Checked = false;
            MouseMove.Checked = false;
            if (comboBoxClients.SelectedItem == null)
                return;
            if (TcpClient.Instance != null)
            {
                await TcpClient.Instance.Send(new ScreenPacket(false));
            }
            await TcpClient.Instance.Send(new SelectClient(comboBoxClients.SelectedItem.ToString()));
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ShowScreen.Checked)
            {
                await TcpClient.Instance.Send(new ScreenPacket(true));
            }
            else
            {
                await TcpClient.Instance.Send(new ScreenPacket(false));
            }
        }

        public void ChangeImage(Bitmap bitmap)
        {
            Form1.Instance.Invoke(new Action(() => Form1.Instance.changeImage(bitmap)));
        }

        private void changeImage(Bitmap bitmap)
        {
            if (pictureBox1 != null)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
                pictureBox1.Image = bitmap;
            }
        }

        private async void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!ShowScreen.Checked)
                return;
            int x = -20;
            int y = -20;
            GetScreenMousePosition(e, ref x, ref y);
            if (e.Button == MouseButtons.Left)
            {
                await TcpClient.Instance.Send(new MousePacket(x, y, MouseOperations.MouseEventFlags.LeftDown));
            }
            else if (e.Button == MouseButtons.Right)
            {
                await TcpClient.Instance.Send(new MousePacket(x, y, MouseOperations.MouseEventFlags.RightDown));
            }
            else if (e.Button == MouseButtons.Middle)
            {
                await TcpClient.Instance.Send(new MousePacket(x, y, MouseOperations.MouseEventFlags.MiddleDown));
            }
        }

        private async void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ShowScreen.Checked)
                return;
            int x = -20;
            int y = -20;
            GetScreenMousePosition(e, ref x, ref y);
            if (e.Button == MouseButtons.Left)
            {
                await TcpClient.Instance.Send(new MousePacket(x, y, MouseOperations.MouseEventFlags.LeftUp));
            }
            else if (e.Button == MouseButtons.Right)
            {
                await TcpClient.Instance.Send(new MousePacket(x, y, MouseOperations.MouseEventFlags.RightUp));
            }
            else if (e.Button == MouseButtons.Middle)
            {
                await TcpClient.Instance.Send(new MousePacket(x, y, MouseOperations.MouseEventFlags.MiddleUp));
            }
        }

        private async void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = -20;
            int y = -20;
            GetScreenMousePosition(e, ref x, ref y);
            if (this.MouseMove.Checked && ShowScreen.Checked)
                await TcpClient.Instance.Send(new MousePacket(x, y, MouseOperations.MouseEventFlags.Move));
        }

        private void GetScreenMousePosition(MouseEventArgs e, ref int X, ref int Y)
        {
            if (!ShowScreen.Checked || pictureBox1.Image == null)
                return;
            Point clickCoordinates = e.Location;
            int xImage = clickCoordinates.X;
            int yImage = clickCoordinates.Y;

            float imageWidth = pictureBox1.Width;
            float imageHeight = pictureBox1.Height;

            float screenWidth = pictureBox1.Image.Width;
            float screenHeight = pictureBox1.Image.Height;

            float widthDifference = screenWidth / imageWidth;
            float heightDifference = screenHeight / imageHeight;

            X = (int)(xImage * widthDifference);
            Y = (int)(yImage * heightDifference);
        }

        private void SendFiles_Click(object sender, EventArgs e)
        {

            Thread newThread = new Thread(new ThreadStart(ThreadMethod));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();

        }

        private TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        private async void ThreadMethod()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                Console.WriteLine("Send: " + selectedFiles.Length + " files");
                foreach (string file in selectedFiles)
                {
                    SendFilePacket filePacket = new SendFilePacket(file);
                    filePacket.TargetDirectory = TargetFolderFile.Text;
                    while (!filePacket.ReadFileInChunks())
                    {
                        await TcpClient.Instance.Send(filePacket);
                        await Task.Delay(10);
                    }
                    await TcpClient.Instance.Send(filePacket);
                }
            }
        }


        private async void SendCMD_Click(object sender, EventArgs e)
        {
            string cmd = CommandInput.Text;
            this.CommandInput.Text = "";
            await TcpClient.Instance.Send(new CommandPacket(cmd));

        }

        public void AddCommandDataText(string cmd)
        {
            Form1.Instance.Invoke(new Action(() => Form1.Instance._AddCommandDataText(cmd)));
        }

        private void _AddCommandDataText(string cmd)
        {
            CommandDataText.Text += cmd;
            CommandDataText.SelectionStart = CommandDataText.Text.Length;
            CommandDataText.ScrollToCaret();
        }

        private ListViewItem SelectedItem { get; set; }

        private async void KillProcess_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null) return;

            ProcessPacket packet = new ProcessPacket(SelectedItem.SubItems[0].Text,
                int.Parse(SelectedItem.SubItems[1].Text),
                SelectedItem.SubItems[2].Text,
                SelectedItem.SubItems[3].Text,
                SelectedItem.SubItems[4].Text);
            await TcpClient.Instance.Send(new KillProcessPacket(packet));
            SelectedItem = null;
        }

        private List<string> KillsOnSpawns = new List<string>();

        private async void KillProcessOnSpawn_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null) return;
            ProcessPacket packet = new ProcessPacket(SelectedItem.SubItems[0].Text,
                int.Parse(SelectedItem.SubItems[1].Text),
                SelectedItem.SubItems[2].Text,
                SelectedItem.SubItems[3].Text,
                SelectedItem.SubItems[4].Text);
            if (KillsOnSpawns.Contains(packet.Name))
                return;
            KillsOnSpawns.Add(packet.Name);
            SelectedItem = null;
            listViewKillOnSpawn.Items.Clear();
            foreach (string item in KillsOnSpawns)
            {
                ListViewItem listViewItem = new ListViewItem(item);
                listViewKillOnSpawn.Items.Add(listViewItem);
            }
            await TcpClient.Instance.Send(new KillProcessPacket(packet));
            await TcpClient.Instance.Send(new KillProcessOnSpawn(KillsOnSpawns));
        }

        private async void RemoveFromKillOnSpawn_Click(object sender, EventArgs e)
        {
            if (listViewKillOnSpawn.SelectedItems.Count > 0)
            {
                string selectedName = listViewKillOnSpawn.SelectedItems[0].Text;
                KillsOnSpawns.Remove(selectedName);
                listViewKillOnSpawn.Items.Clear();
                await TcpClient.Instance.Send(new KillProcessOnSpawn(KillsOnSpawns));
                foreach (string item in KillsOnSpawns)
                {
                    ListViewItem listViewItem = new ListViewItem(item);
                    listViewKillOnSpawn.Items.Add(listViewItem);
                }
            }
        }


        private void ListViewProcesses_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                e.Item.BackColor = Color.FromArgb(100, Color.Blue);
                SelectedItem = e.Item;
            }
            else
            {
                SelectedItem = null;
                e.Item.BackColor = Color.Transparent;
            }
        }



        private bool isUpdatingProcess = false;
        private object updateProcessLock = new object();
        public void UpdateProcess(UpdateProcessPacket updateProcessPacket)
        {
            if (isUpdatingProcess)
            {
                return;
            }

            Form1.Instance.Invoke(new Action(() => Form1.Instance._UpdateProcessAsync(updateProcessPacket)));

        }



        private void _UpdateProcessAsync(UpdateProcessPacket updateProcessPacket)
        {
            lock (updateProcessLock)
            {
                listViewProcesses.BeginUpdate();
                isUpdatingProcess = true;
                HashSet<int> existingProcessIds = new HashSet<int>();

                foreach (ListViewItem item in listViewProcesses.Items)
                {
                    var processId = int.Parse(item.SubItems[1].Text);
                    existingProcessIds.Add(processId);

                    var process = updateProcessPacket.Process.FirstOrDefault(p => p.PID == processId);
                    if (process != null)
                    {
                        item.SubItems[0].Text = process.Name;
                        item.SubItems[2].Text = process.CPU;
                        item.SubItems[3].Text = process.RAM;
                        item.SubItems[4].Text = process.Network;
                    }
                    else
                    {
                        item.Remove();
                    }

                }

                // Ajouter les nouveaux processus
                foreach (var process in updateProcessPacket.Process)
                {
                    if (!existingProcessIds.Contains(process.PID))
                    {
                        string[] rowData = { process.Name, process.PID.ToString(), process.CPU, process.RAM, process.Network };
                        ListViewItem item = new ListViewItem(rowData);
                        listViewProcesses.Items.Add(item);

                    }
                }
                listViewProcesses.EndUpdate();
                isUpdatingProcess = false;
            }
        }






        private bool LoopUpdateProcess = false;
        private async void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == this.ProcessManagement)
            {
                LoopUpdateProcess = true;
                Task.Run(async () =>
                {
                    while (LoopUpdateProcess)
                    {
                        await TcpClient.Instance.Send(new UpdateProcessPacket(new List<ProcessPacket>()));
                        await Task.Delay(1000);
                    }
                });
            }
            if (tabControl.SelectedTab == this.Files)
            {
                if (FileExplorer.Nodes.Count == 0)
                {
                    await TcpClient.Instance.Send(new FileExplorerPacket(""));
                }
            }
        }

        private void TabControl_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == this.ProcessManagement)
            {
                LoopUpdateProcess = false;
            }
        }



        private void tabControl1_DrawItem(Object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabPage tabPage = tabControlProcessGestion.TabPages[e.Index];
            Rectangle tabBounds = tabControlProcessGestion.GetTabRect(e.Index);

            bool isSelected = (e.State == DrawItemState.Selected);
            bool isHot = (e.State == (DrawItemState.Selected | DrawItemState.HotLight));

            // Définir les couleurs pour les onglets sélectionnés et non sélectionnés
            Color selectedTabColor = Color.White;
            Color unselectedTabColor = Color.FromArgb(0, Color.LightGray);

            // Remplir le fond de l'onglet avec la couleur appropriée
            using (SolidBrush brush = new SolidBrush(isSelected ? selectedTabColor : unselectedTabColor))
            {
                g.FillRectangle(brush, tabBounds);
            }

            // Définir la couleur du texte en fonction de l'état de l'onglet
            Color textColor = (isSelected || isHot) ? Color.Black : Color.Black;

            // Dessiner le texte de l'onglet
            using (Brush textBrush = new SolidBrush(textColor))
            {
                using (StringFormat stringFormat = new StringFormat())
                {
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    g.DrawString(tabPage.Text, e.Font, textBrush, tabBounds, stringFormat);
                }
            }
        }

        private async void HandHandKillOnSpawn_Click(object sender, EventArgs e)
        {
            string Name = NameOfNewKillOnSpawn.Text;
            NameOfNewKillOnSpawn.Text = string.Empty;
            KillsOnSpawns.Add(Name);
            listViewKillOnSpawn.Items.Clear();
            foreach (string item in KillsOnSpawns)
            {
                ListViewItem listViewItem = new ListViewItem(item);
                listViewKillOnSpawn.Items.Add(listViewItem);
            }
            await TcpClient.Instance.Send(new KillProcessOnSpawn(KillsOnSpawns));
        }

        private async void CreateNewFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new InputBox("New Folder Name"))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string folderName = dialog.InputText;
                    await TcpClient.Instance.Send(new CreateFolderPacket(TargetFolderFile.Text, folderName));
                }
            }
        }

    }
}