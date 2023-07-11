
using Giny.Core.DesignPattern;

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
            ((System.ComponentModel.ISupportInitialize)tcpServerBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabControl.SuspendLayout();
            Screen.SuspendLayout();
            Files.SuspendLayout();
            Commands.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxClients
            // 
            comboBoxClients.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            comboBoxClients.DisplayMember = "ID";
            comboBoxClients.FormattingEnabled = true;
            comboBoxClients.Location = new Point(12, 3);
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
            SendCMD.Location = new Point(681, 379);
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
            tabControl.Location = new Point(-3, 32);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(780, 434);
            tabControl.TabIndex = 6;
            // 
            // Screen
            // 
            Screen.Controls.Add(pictureBox1);
            Screen.Controls.Add(ShowScreen);
            Screen.Controls.Add(MouseMove);
            Screen.Location = new Point(4, 24);
            Screen.Name = "Screen";
            Screen.Padding = new Padding(3);
            Screen.Size = new Size(772, 406);
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
            Files.Size = new Size(772, 406);
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
            Commands.Size = new Size(772, 406);
            Commands.TabIndex = 2;
            Commands.Text = "Commands";
            Commands.UseVisualStyleBackColor = true;
            // 
            // CommandDataText
            // 
            CommandDataText.Location = new Point(9, 12);
            CommandDataText.Multiline = true;
            CommandDataText.Name = "CommandDataText";
            CommandDataText.ReadOnly = true;
            CommandDataText.ScrollBars = ScrollBars.Vertical;
            CommandDataText.Size = new Size(751, 362);
            CommandDataText.TabIndex = 7;
            // 
            // CommandInput
            // 
            CommandInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CommandInput.Location = new Point(9, 379);
            CommandInput.Name = "CommandInput";
            CommandInput.Size = new Size(666, 23);
            CommandInput.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(773, 463);
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
            ResumeLayout(false);
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
    }
}