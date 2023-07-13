using System;
using System.Windows.Forms;

namespace FlexiLeaf.ControlHub.Interfaces
{
    public partial class InputBox : Form
    {
        public InputBox(string text)
        {
            InitializeComponent();
        }

        public string InputText
        {
            get { return textBox1.Text; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
