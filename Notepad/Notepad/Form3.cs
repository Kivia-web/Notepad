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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            RichTextBox richtextbox = Form1.selectedrtb;
            richtextbox.SaveFile(Application.StartupPath + "\\Files\\" + path + ".rtf");
            Form1.selectedrtb = richtextbox;
            this.Dispose();
            this.Close();
        }
    }
}
