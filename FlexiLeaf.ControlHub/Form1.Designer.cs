
using Giny.Core.DesignPattern;
using System.Windows.Forms;

namespace FlexiLeaf.ControlHub
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            comboBoxClients = new ComboBox();
            tcpServerBindingSource = new BindingSource(components);
            pictureBox1 = new PictureBox();
            ShowScreen = new CheckBox();
            MouseMove = new CheckBox();
            SendFiles = new Button();
            SendCMD = new Button();
            tabControl = new TabControl();
            Screen = new TabPage();
            Files = new TabPage();
            Commands = new TabPage();
            CommandDataText = new TextBox();
            CommandInput = new TextBox();
            ProcessManagement = new TabPage();
            tabControlProcessGestion = new TabControl();
            tabProcess = new TabPage();
            listViewProcesses = new ListView();
            HeaderName = new ColumnHeader();
            HeaderPID = new ColumnHeader();
            HeaderCPU = new ColumnHeader();
            HeaderRAM = new ColumnHeader();
            HeaderNetwork = new ColumnHeader();
            contextMenu = new ContextMenuStrip(components);
            menuItem1 = new ToolStripMenuItem();
            menuItem2 = new ToolStripMenuItem();
            KillProcess = new Button();
            tabOptions = new TabPage();
            HandHandKillOnSpawn = new Button();
            label3 = new Label();
            NameOfNewKillOnSpawn = new TextBox();
            label2 = new Label();
            label1 = new Label();
            listViewKillOnSpawn = new ListView();
            OptionHeaderName = new ColumnHeader();
            contextMenuStripKillOnSpawn = new ContextMenuStrip(components);
            removeMenuItem = new ToolStripMenuItem();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)tcpServerBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabControl.SuspendLayout();
            Screen.SuspendLayout();
            Files.SuspendLayout();
            Commands.SuspendLayout();
            ProcessManagement.SuspendLayout();
            tabControlProcessGestion.SuspendLayout();
            tabProcess.SuspendLayout();
            contextMenu.SuspendLayout();
            tabOptions.SuspendLayout();
            contextMenuStripKillOnSpawn.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxClients
            // 
            comboBoxClients.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            comboBoxClients.DisplayMember = "ID";
            comboBoxClients.FormattingEnabled = true;
            comboBoxClients.Location = new Point(45, 6);
            comboBoxClients.Name = "comboBoxClients";
            comboBoxClients.Size = new Size(262, 23);
            comboBoxClients.TabIndex = 0;
            comboBoxClients.ValueMember = "ID";
            comboBoxClients.SelectedIndexChanged += comboBoxClients_SelectedIndexChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(-4, 28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(776, 378);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // ShowScreen
            // 
            ShowScreen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ShowScreen.AutoSize = true;
            ShowScreen.Location = new Point(6, 3);
            ShowScreen.Name = "ShowScreen";
            ShowScreen.Size = new Size(93, 19);
            ShowScreen.TabIndex = 2;
            ShowScreen.Text = "Show Screen";
            ShowScreen.UseVisualStyleBackColor = true;
            ShowScreen.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // MouseMove
            // 
            MouseMove.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MouseMove.AutoSize = true;
            MouseMove.Location = new Point(105, 3);
            MouseMove.Name = "MouseMove";
            MouseMove.Size = new Size(95, 19);
            MouseMove.TabIndex = 3;
            MouseMove.Text = "Mouse Move";
            MouseMove.UseVisualStyleBackColor = true;
            // 
            // SendFiles
            // 
            SendFiles.Location = new Point(11, 6);
            SendFiles.Name = "SendFiles";
            SendFiles.Size = new Size(113, 31);
            SendFiles.TabIndex = 4;
            SendFiles.Text = "Send Files";
            SendFiles.UseVisualStyleBackColor = true;
            SendFiles.Click += SendFiles_Click;
            // 
            // SendCMD
            // 
            SendCMD.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SendCMD.Location = new Point(684, 365);
            SendCMD.Name = "SendCMD";
            SendCMD.Size = new Size(79, 23);
            SendCMD.TabIndex = 5;
            SendCMD.Text = "Send CMD";
            SendCMD.UseVisualStyleBackColor = true;
            SendCMD.Click += SendCMD_Click;
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(Screen);
            tabControl.Controls.Add(Files);
            tabControl.Controls.Add(Commands);
            tabControl.Controls.Add(ProcessManagement);
            tabControl.Location = new Point(-3, 32);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(775, 418);
            tabControl.TabIndex = 6;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            tabControl.Deselecting += TabControl_Deselecting;
            // 
            // Screen
            // 
            Screen.Controls.Add(pictureBox1);
            Screen.Controls.Add(ShowScreen);
            Screen.Controls.Add(MouseMove);
            Screen.Location = new Point(4, 24);
            Screen.Name = "Screen";
            Screen.Padding = new Padding(3);
            Screen.Size = new Size(767, 390);
            Screen.TabIndex = 0;
            Screen.Text = "Screen";
            Screen.UseVisualStyleBackColor = true;
            // 
            // Files
            // 
            Files.Controls.Add(SendFiles);
            Files.Location = new Point(4, 24);
            Files.Name = "Files";
            Files.Padding = new Padding(3);
            Files.Size = new Size(767, 390);
            Files.TabIndex = 1;
            Files.Text = "Files";
            Files.UseVisualStyleBackColor = true;
            // 
            // Commands
            // 
            Commands.Controls.Add(CommandDataText);
            Commands.Controls.Add(CommandInput);
            Commands.Controls.Add(SendCMD);
            Commands.Location = new Point(4, 24);
            Commands.Name = "Commands";
            Commands.Size = new Size(767, 390);
            Commands.TabIndex = 2;
            Commands.Text = "Commands";
            Commands.UseVisualStyleBackColor = true;
            // 
            // CommandDataText
            // 
            CommandDataText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CommandDataText.Location = new Point(3, 12);
            CommandDataText.Multiline = true;
            CommandDataText.Name = "CommandDataText";
            CommandDataText.ReadOnly = true;
            CommandDataText.ScrollBars = ScrollBars.Vertical;
            CommandDataText.Size = new Size(757, 351);
            CommandDataText.TabIndex = 7;
            // 
            // CommandInput
            // 
            CommandInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CommandInput.Location = new Point(3, 365);
            CommandInput.Name = "CommandInput";
            CommandInput.Size = new Size(672, 23);
            CommandInput.TabIndex = 6;
            // 
            // ProcessManagement
            // 
            ProcessManagement.Controls.Add(tabControlProcessGestion);
            ProcessManagement.Location = new Point(4, 24);
            ProcessManagement.Name = "ProcessManagement";
            ProcessManagement.Size = new Size(767, 390);
            ProcessManagement.TabIndex = 3;
            ProcessManagement.Text = "Process Management";
            ProcessManagement.UseVisualStyleBackColor = true;
            // 
            // tabControlProcessGestion
            // 
            tabControlProcessGestion.Alignment = TabAlignment.Left;
            tabControlProcessGestion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlProcessGestion.Controls.Add(tabProcess);
            tabControlProcessGestion.Controls.Add(tabOptions);
            tabControlProcessGestion.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlProcessGestion.ItemSize = new Size(30, 96);
            tabControlProcessGestion.Location = new Point(3, 0);
            tabControlProcessGestion.Multiline = true;
            tabControlProcessGestion.Name = "tabControlProcessGestion";
            tabControlProcessGestion.SelectedIndex = 0;
            tabControlProcessGestion.Size = new Size(769, 396);
            tabControlProcessGestion.SizeMode = TabSizeMode.Fixed;
            tabControlProcessGestion.TabIndex = 2;
            tabControlProcessGestion.DrawItem += tabControl1_DrawItem;
            // 
            // tabProcess
            // 
            tabProcess.Controls.Add(listViewProcesses);
            tabProcess.Controls.Add(KillProcess);
            tabProcess.Location = new Point(100, 4);
            tabProcess.Name = "tabProcess";
            tabProcess.Padding = new Padding(3);
            tabProcess.Size = new Size(665, 388);
            tabProcess.TabIndex = 0;
            tabProcess.Text = "Process";
            tabProcess.UseVisualStyleBackColor = true;
            // 
            // listViewProcesses
            // 
            listViewProcesses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewProcesses.Columns.AddRange(new ColumnHeader[] { HeaderName, HeaderPID, HeaderCPU, HeaderRAM, HeaderNetwork });
            listViewProcesses.ContextMenuStrip = contextMenu;
            listViewProcesses.FullRowSelect = true;
            listViewProcesses.Location = new Point(0, 0);
            listViewProcesses.Name = "listViewProcesses";
            listViewProcesses.Size = new Size(669, 363);
            listViewProcesses.TabIndex = 0;
            listViewProcesses.UseCompatibleStateImageBehavior = false;
            listViewProcesses.View = View.Details;
            listViewProcesses.ItemSelectionChanged += ListViewProcesses_ItemSelectionChanged;
            // 
            // HeaderName
            // 
            HeaderName.Text = "Name";
            HeaderName.Width = 150;
            // 
            // HeaderPID
            // 
            HeaderPID.Text = "PID";
            // 
            // HeaderCPU
            // 
            HeaderCPU.Text = "CPU";
            HeaderCPU.Width = 100;
            // 
            // HeaderRAM
            // 
            HeaderRAM.Text = "RAM";
            HeaderRAM.Width = 100;
            // 
            // HeaderNetwork
            // 
            HeaderNetwork.Text = "Network";
            HeaderNetwork.Width = 100;
            // 
            // contextMenu
            // 
            contextMenu.Items.AddRange(new ToolStripItem[] { menuItem1, menuItem2 });
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new Size(145, 48);
            // 
            // menuItem1
            // 
            menuItem1.Name = "menuItem1";
            menuItem1.Size = new Size(144, 22);
            menuItem1.Text = "Kill Process";
            menuItem1.Click += KillProcess_Click;
            // 
            // menuItem2
            // 
            menuItem2.Name = "menuItem2";
            menuItem2.Size = new Size(144, 22);
            menuItem2.Text = "Kill on spawn";
            menuItem2.Click += KillProcessOnSpawn_Click;
            // 
            // KillProcess
            // 
            KillProcess.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            KillProcess.Location = new Point(582, 364);
            KillProcess.Name = "KillProcess";
            KillProcess.Size = new Size(75, 23);
            KillProcess.TabIndex = 1;
            KillProcess.Text = "Kill Process";
            KillProcess.UseVisualStyleBackColor = true;
            KillProcess.Click += KillProcess_Click;
            // 
            // tabOptions
            // 
            tabOptions.Controls.Add(HandHandKillOnSpawn);
            tabOptions.Controls.Add(label3);
            tabOptions.Controls.Add(NameOfNewKillOnSpawn);
            tabOptions.Controls.Add(label2);
            tabOptions.Controls.Add(label1);
            tabOptions.Controls.Add(listViewKillOnSpawn);
            tabOptions.Location = new Point(100, 4);
            tabOptions.Name = "tabOptions";
            tabOptions.Padding = new Padding(3);
            tabOptions.Size = new Size(665, 388);
            tabOptions.TabIndex = 1;
            tabOptions.Text = "Options";
            tabOptions.UseVisualStyleBackColor = true;
            // 
            // HandHandKillOnSpawn
            // 
            HandHandKillOnSpawn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            HandHandKillOnSpawn.Location = new Point(104, 330);
            HandHandKillOnSpawn.Name = "HandHandKillOnSpawn";
            HandHandKillOnSpawn.Size = new Size(66, 23);
            HandHandKillOnSpawn.TabIndex = 5;
            HandHandKillOnSpawn.Text = "Add";
            HandHandKillOnSpawn.UseVisualStyleBackColor = true;
            HandHandKillOnSpawn.Click += HandHandKillOnSpawn_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(6, 304);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 4;
            label3.Text = "Name:";
            // 
            // NameOfNewKillOnSpawn
            // 
            NameOfNewKillOnSpawn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            NameOfNewKillOnSpawn.Location = new Point(55, 301);
            NameOfNewKillOnSpawn.Name = "NameOfNewKillOnSpawn";
            NameOfNewKillOnSpawn.Size = new Size(115, 23);
            NameOfNewKillOnSpawn.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(5, 283);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 2;
            label2.Text = "Add Kill on Spawn:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 15);
            label1.Name = "label1";
            label1.Size = new Size(149, 15);
            label1.TabIndex = 1;
            label1.Text = "Processes to Kill on Spawn:";
            // 
            // listViewKillOnSpawn
            // 
            listViewKillOnSpawn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listViewKillOnSpawn.Columns.AddRange(new ColumnHeader[] { OptionHeaderName });
            listViewKillOnSpawn.ContextMenuStrip = contextMenuStripKillOnSpawn;
            listViewKillOnSpawn.Location = new Point(6, 33);
            listViewKillOnSpawn.Name = "listViewKillOnSpawn";
            listViewKillOnSpawn.Size = new Size(164, 247);
            listViewKillOnSpawn.TabIndex = 0;
            listViewKillOnSpawn.UseCompatibleStateImageBehavior = false;
            listViewKillOnSpawn.View = View.Details;
            // 
            // OptionHeaderName
            // 
            OptionHeaderName.Text = "Name";
            OptionHeaderName.Width = 150;
            // 
            // contextMenuStripKillOnSpawn
            // 
            contextMenuStripKillOnSpawn.Items.AddRange(new ToolStripItem[] { removeMenuItem });
            contextMenuStripKillOnSpawn.Name = "contextMenuStripKillOnSpawn";
            contextMenuStripKillOnSpawn.Size = new Size(118, 26);
            // 
            // removeMenuItem
            // 
            removeMenuItem.Name = "removeMenuItem";
            removeMenuItem.Size = new Size(117, 22);
            removeMenuItem.Text = "Remove";
            removeMenuItem.Click += RemoveFromKillOnSpawn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(4, 9);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 7;
            label4.Text = "Client:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(768, 447);
            Controls.Add(label4);
            Controls.Add(tabControl);
            Controls.Add(comboBoxClients);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)tcpServerBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabControl.ResumeLayout(false);
            Screen.ResumeLayout(false);
            Screen.PerformLayout();
            Files.ResumeLayout(false);
            Commands.ResumeLayout(false);
            Commands.PerformLayout();
            ProcessManagement.ResumeLayout(false);
            tabControlProcessGestion.ResumeLayout(false);
            tabProcess.ResumeLayout(false);
            contextMenu.ResumeLayout(false);
            tabOptions.ResumeLayout(false);
            tabOptions.PerformLayout();
            contextMenuStripKillOnSpawn.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox comboBoxClients;
        private BindingSource tcpServerBindingSource;
        private PictureBox pictureBox1;
        private CheckBox ShowScreen;
        private CheckBox MouseMove;
        private Button SendFiles;
        private Button SendCMD;
        private TabControl tabControl;
        private TabPage Screen;
        private TabPage Files;
        private TabPage Commands;
        private TextBox CommandInput;
        private TextBox CommandDataText;
        private TabPage ProcessManagement;
        private ListView listViewProcesses;
        private Button KillProcess;
        private ColumnHeader HeaderName;
        private ColumnHeader HeaderRAM;
        private ColumnHeader HeaderCPU;
        private ColumnHeader HeaderNetwork;
        private ColumnHeader HeaderPID;
        private TabControl tabControlProcessGestion;
        private TabPage tabProcess;
        private TabPage tabOptions;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItem1;
        private ToolStripMenuItem menuItem2;
        private ListView listViewKillOnSpawn;
        private ColumnHeader OptionHeaderName;
        private Label label1;
        private ContextMenuStrip contextMenuStripKillOnSpawn;
        private ToolStripMenuItem removeMenuItem;
        private Label label2;
        private Button HandHandKillOnSpawn;
        private Label label3;
        private TextBox NameOfNewKillOnSpawn;
        private Label label4;
    }
}