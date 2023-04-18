using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RichTextBox richtextbox = Form1.selectedrtb;
            float size = Convert.ToSingle(textBox1.Text);
            richtextbox.SelectionFont = new Font(richtextbox.SelectionFont.FontFamily, size, richtextbox.SelectionFont.Style);
            this.Dispose();
            this.Close();
        }
    }
}
