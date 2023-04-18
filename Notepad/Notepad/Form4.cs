using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            string[] allfiles = Directory.GetFiles(Application.StartupPath + "\\Files\\");
            foreach (string filename in allfiles)
            {
                listBox1.Items.Add(Path.GetFileName(filename));
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path;
            try
            {
                path = Application.StartupPath + "\\Files\\" + listBox1.SelectedItem.ToString();
                TabPage p = new TabPage(listBox1.SelectedItem.ToString());
                Form1.tabControl.TabPages.Add(p);
                RichTextBox richtextbox = new RichTextBox();
                p.Controls.Add(richtextbox);
                Form1 form1 = new Form1();
                Form1.selectedrtb = richtextbox;
                richtextbox.Location = new Point(100, 100);
                richtextbox.Visible = true;
                richtextbox.Multiline = true;
                richtextbox.Dock = DockStyle.Fill;
                richtextbox.ContextMenuStrip = form1.contextMenuStrip1;
                richtextbox.LoadFile(path);
                this.Dispose();
                this.Close();
                
            }
            catch (Exception Unknow_error)
            {
                MessageBox.Show(Unknow_error.Message, "Неизвестная ошибка", MessageBoxButtons.OK);
            }

        }
    }
}
