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
                _instance ??= new Form1();
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
            await TcpClient.Instance.Connect(AppSettings.Instance.IPAddress, AppSettings.Instance.Port, AppSettings.Instance.Password);
        }

        public void RefreshClientList()
        {
            Invoke(new Action(refreshClientList));
        }
        private void refreshClientList()
        {
            comboBoxClients.DataSource = null;
            comboBoxClients.DataSource = Handlers.ServerHandlers.Clients;
        }

        private async void comboBoxClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScreenTab.ResetScreen();
            if (comboBoxClients.SelectedItem == null)
                return;
            if (TcpClient.Instance != null)
            {
                await TcpClient.Instance.Send(new ScreenPacket(false, ScreenTab.Fps, ScreenTab.Quality));
            }
            await TcpClient.Instance.Send(new SelectClient(comboBoxClients.SelectedItem.ToString()));
        }

        


        private async void SendCMD_Click(object sender, EventArgs e)
        {
            string cmd = CommandInput.Text;
            this.CommandInput.Text = "";
            await TcpClient.Instance.Send(new CommandPacket(cmd));

        }

        public void AddCommandDataText(string cmd)
        {
            Instance.Invoke(new Action(() => Form1.Instance.addCommandDataText(cmd)));
        }

        private void addCommandDataText(string cmd)
        {
            CommandDataText.Text += cmd;
            CommandDataText.SelectionStart = CommandDataText.Text.Length;
            CommandDataText.ScrollToCaret();
        }

        private ListViewItem SelectedItem { get; set; }

        private async void KillProcess_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null) return;

            ProcessPacket packet = new(SelectedItem.SubItems[0].Text,
                int.Parse(SelectedItem.SubItems[1].Text),
                SelectedItem.SubItems[2].Text,
                SelectedItem.SubItems[3].Text,
                SelectedItem.SubItems[4].Text);
            await TcpClient.Instance.Send(new KillProcessPacket(packet));
            SelectedItem = null;
        }

        private readonly List<string> KillsOnSpawns = new();

        private async void KillProcessOnSpawn_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null) return;
            ProcessPacket packet = new(SelectedItem.SubItems[0].Text,
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
                ListViewItem listViewItem = new(item);
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
                    ListViewItem listViewItem = new(item);
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
        private readonly object updateProcessLock = new();
        public void UpdateProcess(UpdateProcessPacket updateProcessPacket)
        {
            if (isUpdatingProcess)
            {
                return;
            }

            Form1.Instance.Invoke(new Action(() => Form1.Instance.updateProcessAsync(updateProcessPacket)));

        }

        private void updateProcessAsync(UpdateProcessPacket updateProcessPacket)
        {
            lock (updateProcessLock)
            {
                listViewProcesses.BeginUpdate();
                isUpdatingProcess = true;
                HashSet<int> existingProcessIds = new();

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
                        ListViewItem item = new(rowData);
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
                _ = Task.Run(async () =>
                {
                    while (LoopUpdateProcess)
                    {
                        await TcpClient.Instance.Send(new UpdateProcessPacket(new List<ProcessPacket>()));
                        await Task.Delay(1000);
                    }
                });
            }
            if (tabControl.SelectedTab == this.FilesTab)
            {
                if (FilesTab.FileExplorerEmpty)
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
            using (SolidBrush brush = new(isSelected ? selectedTabColor : unselectedTabColor))
            {
                g.FillRectangle(brush, tabBounds);
            }

            // Définir la couleur du texte en fonction de l'état de l'onglet
            Color textColor = (isSelected || isHot) ? Color.Black : Color.Black;

            // Dessiner le texte de l'onglet
            using Brush textBrush = new SolidBrush(textColor);
            using StringFormat stringFormat = new();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(tabPage.Text, e.Font, textBrush, tabBounds, stringFormat);
        }

        private async void HandHandKillOnSpawn_Click(object sender, EventArgs e)
        {
            string Name = NameOfNewKillOnSpawn.Text;
            NameOfNewKillOnSpawn.Text = string.Empty;
            KillsOnSpawns.Add(Name);
            listViewKillOnSpawn.Items.Clear();
            foreach (string item in KillsOnSpawns)
            {
                ListViewItem listViewItem = new(item);
                listViewKillOnSpawn.Items.Add(listViewItem);
            }
            await TcpClient.Instance.Send(new KillProcessOnSpawn(KillsOnSpawns));
        }




    }
}