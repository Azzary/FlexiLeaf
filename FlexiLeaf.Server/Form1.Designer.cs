
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
            ((System.ComponentModel.ISupportInitialize)tcpServerBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // comboBoxClients
            // 
            comboBoxClients.DisplayMember = "ID";
            comboBoxClients.FormattingEnabled = true;
            comboBoxClients.Location = new Point(19, 18);
            comboBoxClients.Name = "comboBoxClients";
            comboBoxClients.Size = new Size(121, 23);
            comboBoxClients.TabIndex = 0;
            comboBoxClients.ValueMember = "ID";
            comboBoxClients.SelectedIndexChanged += comboBoxClients_SelectedIndexChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(12, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(776, 391);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // ShowScreen
            // 
            ShowScreen.AutoSize = true;
            ShowScreen.Location = new Point(146, 20);
            ShowScreen.Name = "ShowScreen";
            ShowScreen.Size = new Size(93, 19);
            ShowScreen.TabIndex = 2;
            ShowScreen.Text = "Show Screen";
            ShowScreen.UseVisualStyleBackColor = true;
            ShowScreen.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // MouseMove
            // 
            MouseMove.AutoSize = true;
            MouseMove.Location = new Point(237, 21);
            MouseMove.Name = "MouseMove";
            MouseMove.Size = new Size(95, 19);
            MouseMove.TabIndex = 3;
            MouseMove.Text = "Mouse Move";
            MouseMove.UseVisualStyleBackColor = true;
            // 
            // SendFiles
            // 
            SendFiles.Location = new Point(338, 18);
            SendFiles.Name = "SendFiles";
            SendFiles.Size = new Size(75, 23);
            SendFiles.TabIndex = 4;
            SendFiles.Text = "Send Files";
            SendFiles.UseVisualStyleBackColor = true;
            SendFiles.Click += SendFiles_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(SendFiles);
            Controls.Add(MouseMove);
            Controls.Add(ShowScreen);
            Controls.Add(pictureBox1);
            Controls.Add(comboBoxClients);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)tcpServerBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
    }
}