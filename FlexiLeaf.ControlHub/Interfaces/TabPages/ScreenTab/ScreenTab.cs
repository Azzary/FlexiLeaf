using FlexiLeaf.Core.Network;
using FlexiLeaf.Core.Network.Packets;
using FlexiLeaf.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexiLeaf.ControlHub.Interfaces.TabPages.ScreenTab
{
    public class ScreenTab : TabPage
    {

        private ComboBox QualitySelector = new();
        private Label labelFps = new();
        private TextBox FpsInputBox = new();
        private PictureBox pictureBox1 = new();
        private CheckBox ShowScreen =new();
        private new readonly CheckBox MouseMove = new();

        public ScreenTab() 
        {
            Location = new Point(4, 24);
            Name = "Screen";
            Padding = new Padding(3);
            Size = new Size(767, 390);
            TabIndex = 0;
            Text = "Screen";
            SuspendLayout();
            ResumeLayout(false);
            PerformLayout();
            UseVisualStyleBackColor = true;
            Controls.Add(QualitySelector);
            Controls.Add(labelFps);
            Controls.Add(FpsInputBox);
            Controls.Add(pictureBox1);
            Controls.Add(ShowScreen);
            Controls.Add(MouseMove);
            Init();
        }

        public void Init()
        {
            // 
            // QualitySelector
            // 
            QualitySelector.FormattingEnabled = true;
            QualitySelector.Items.AddRange(new object[] { "100%", "90%", "80%", "70%", "60%", "50%", "40%", "30%", "20%", "10%" });
            QualitySelector.Location = new Point(284, 4);
            QualitySelector.Name = "QualitySelector";
            QualitySelector.Size = new Size(121, 23);
            QualitySelector.TabIndex = 6;
            QualitySelector.SelectedIndex = 0;
            QualitySelector.SelectedIndexChanged += QualitySelector_SelectedIndexChanged;
            // 
            // labelFps
            // 
            labelFps.AutoSize = true;
            labelFps.Location = new Point(206, 7);
            labelFps.Name = "labelFps";
            labelFps.Size = new Size(32, 15);
            labelFps.TabIndex = 5;
            labelFps.Text = "FPS: ";
            // 
            // FpsInputBox
            // 
            FpsInputBox.Location = new Point(235, 4);
            FpsInputBox.Name = "FpsInputBox";
            FpsInputBox.Size = new Size(43, 23);
            FpsInputBox.TabIndex = 4;
            FpsInputBox.TextChanged += FpsInputBox_TextChanged;
            FpsInputBox.Text = "20";
            // 
            // pictureBox1
            // 
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            // 
            // ShowScreen
            // 
            ShowScreen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ShowScreen.AutoSize = true;
            ShowScreen.Location = new Point(6, 6);
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
            MouseMove.Location = new Point(105, 6);
            MouseMove.Name = "MouseMove";
            MouseMove.Size = new Size(95, 19);
            MouseMove.TabIndex = 3;
            MouseMove.Text = "Mouse Move";
            MouseMove.UseVisualStyleBackColor = true;
        }

        public void ResetScreen()
        {
            ShowScreen.Checked = false;
            MouseMove.Checked = false;
        }

        public void ChangeImage(Bitmap bitmap)
        {
            this.Invoke(new Action(() => changeImage(bitmap)));
        }

        public short Fps => short.Parse(FpsInputBox.Text);

        public short Quality => short.Parse(QualitySelector.Text[..^1]);
        

        private async void FpsInputBox_TextChanged(object sender, EventArgs e)
        {

            if (int.TryParse(FpsInputBox.Text, out int value))
            {
                if (value < 0)
                {
                    FpsInputBox.Text = "1";
                }
                else if (value >= 60)
                {
                    FpsInputBox.Text = "60";
                }
                else if (ShowScreen.Checked)
                {
                    await TcpClient.Instance.Send(new ScreenPacket(true, (short)value, Quality));
                }
            }
            else
            {
                FpsInputBox.Text = "10";
            }
        }

        private async void QualitySelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            await TcpClient.Instance.Send(new ScreenPacket(true, short.Parse(FpsInputBox.Text), Quality));
        }

        private void changeImage(Bitmap bitmap)
        {
            if (pictureBox1 != null)
            {
                pictureBox1.Image?.Dispose();
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

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ShowScreen.Checked)
            {
                await TcpClient.Instance.Send(new ScreenPacket(true, short.Parse(FpsInputBox.Text), Quality));
            }
            else
            {
                await TcpClient.Instance.Send(new ScreenPacket(false, short.Parse(FpsInputBox.Text), Quality));
            }
        }
    }
}
