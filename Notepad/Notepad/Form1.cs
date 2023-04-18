
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Notepad
{
    public partial class Form1 : Form
    {
        public static RichTextBox selectedrtb = new RichTextBox();
        public static TabControl tabControl = new TabControl();
        public Form1()
        {
            InitializeComponent();
            TabPage p = tabControl1.SelectedTab;
            selectedrtb = p.Controls.OfType<RichTextBox>().FirstOrDefault();
            ToolStripMenuItem copyMenuItem = new ToolStripMenuItem("Копировать");
            ToolStripMenuItem pasteMenuItem = new ToolStripMenuItem("Вставить");
            ToolStripMenuItem undoMenuItem = new ToolStripMenuItem("Отменить");
            ToolStripMenuItem redoMenuItem = new ToolStripMenuItem("Вернуть");
            ToolStripMenuItem pasteimageMenuItem = new ToolStripMenuItem("Вставить изображение");
            ToolStripMenuItem fontMenuItem = new ToolStripMenuItem("Шрифт");
            ToolStripMenuItem fontboldMenuItem = new ToolStripMenuItem("Жирный");
            ToolStripMenuItem fontitalicMenuItem = new ToolStripMenuItem("Курсив");
            ToolStripMenuItem fontulineMenuItem = new ToolStripMenuItem("Подчёркнутный");
            ToolStripMenuItem fontsizeMenuItem = new ToolStripMenuItem("Размер шрифта");
            fontMenuItem.DropDownItems.Add(fontboldMenuItem);
            fontMenuItem.DropDownItems.Add(fontitalicMenuItem);
            fontMenuItem.DropDownItems.Add(fontulineMenuItem);
            fontMenuItem.DropDownItems.Add(fontsizeMenuItem);
            contextMenuStrip1.Items.AddRange(new[] { copyMenuItem, pasteMenuItem, undoMenuItem, 
                redoMenuItem,pasteimageMenuItem, fontMenuItem});
            richTextBox1.ContextMenuStrip = contextMenuStrip1;
            selectedrtb = richTextBox1;
            copyMenuItem.Click += copyMenuItem_Click;
            pasteMenuItem.Click += pasteMenuItem_Click;
            pasteimageMenuItem.Click += pasteimageMenuItem_Click;
            undoMenuItem.Click += undoMenuItem_Click;
            redoMenuItem.Click += redoMenuItem_Click;
            fontboldMenuItem.Click += fontboldMenuItem_Click;
            fontitalicMenuItem.Click += fontitalicMenuItem_Click;
            fontulineMenuItem.Click += fontulineMenuItem_Click;
            fontsizeMenuItem.Click += fontsizeMenuItem_Click;
            richTextBox1.Text = "\n Хрю";
            InsertImage(System.Drawing.Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\Хрю.png"), richTextBox1);
        }


        void pasteMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richtextbox = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl as RichTextBox;
            selectedrtb = richtextbox;
            richtextbox.Paste();
        }
        void pasteimageMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richtextbox = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl as RichTextBox;
            selectedrtb = richtextbox;
            OpenFileDialog opimg = new OpenFileDialog();
            if (opimg.FileName == null) return;
            if (opimg.ShowDialog() == DialogResult.OK)
                try
                {
                InsertImage(System.Drawing.Image.FromFile(opimg.FileName), richtextbox);
                }
                catch
                {
                MessageBox.Show("Неверный формат файла","Ошибка", MessageBoxButtons.OK);
                }
        }
        void copyMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richtextbox = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl as RichTextBox;
            selectedrtb = richtextbox;
            richtextbox.Copy();
        }
        void undoMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richtextbox = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl as RichTextBox;
            selectedrtb = richtextbox;
            richtextbox.Undo();
        }
        void redoMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richtextbox = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl as RichTextBox;
            selectedrtb = richtextbox;
            richtextbox.Redo();
        }
        void fontboldMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richtextbox = ((ContextMenuStrip)((ToolStripMenuItem)((ToolStripDropDownItem)sender).OwnerItem).Owner).SourceControl as RichTextBox;
            selectedrtb = richtextbox;
            richtextbox.SelectionFont = new Font(richtextbox.SelectionFont, richtextbox.SelectionFont.Style ^ FontStyle.Bold);
        }
        void fontitalicMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richtextbox = ((ContextMenuStrip)((ToolStripMenuItem)((ToolStripDropDownItem)sender).OwnerItem).Owner).SourceControl as RichTextBox;
            selectedrtb = richtextbox;
            richtextbox.SelectionFont = new Font(richtextbox.SelectionFont, richtextbox.SelectionFont.Style ^ FontStyle.Italic);
        }
        void fontulineMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richtextbox = ((ContextMenuStrip)((ToolStripMenuItem)((ToolStripDropDownItem)sender).OwnerItem).Owner).SourceControl as RichTextBox;
            selectedrtb = richtextbox;
            richtextbox.SelectionFont = new Font(richtextbox.SelectionFont, richtextbox.SelectionFont.Style ^ FontStyle.Underline);
        }
        void fontsizeMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richtextbox = ((ContextMenuStrip)((ToolStripMenuItem)((ToolStripDropDownItem)sender).OwnerItem).Owner).SourceControl as RichTextBox;
            selectedrtb = richtextbox;
            Form2 form2 = new Form2();
            form2.Show();
        }
        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab.Text = "Новая заметка";
            TabPage p = tabControl1.SelectedTab;
            RichTextBox richtextbox = p.Controls.OfType<RichTextBox>().FirstOrDefault();
            selectedrtb = richtextbox;
            richtextbox.Clear();
        }
        internal void InsertImage(System.Drawing.Image img,RichTextBox richtextbox)
        {
            IDataObject obj = Clipboard.GetDataObject();
            object data;
            try
            {
                if (obj.GetDataPresent(DataFormats.Text))
                {
                    data = Clipboard.GetText();
                    Clipboard.Clear();
                    Clipboard.SetImage(img);
                    richtextbox.Paste();
                    selectedrtb = richtextbox;
                    Clipboard.Clear();
                    Clipboard.SetDataObject(data, true);
                }
                else
                {
                    data = new Bitmap(Clipboard.GetFileDropList()[0].ToString());
                    Clipboard.Clear();
                    Clipboard.SetImage(img);
                    selectedrtb = richtextbox;
                    richtextbox.Paste();
                    Clipboard.Clear();
                    Clipboard.SetImage((System.Drawing.Image)data);
                }
            }
            catch
            {
                Clipboard.Clear();
                Clipboard.SetImage(img);
                selectedrtb = richtextbox;
                richtextbox.Paste();
                Clipboard.Clear();
            }
        }

        private void открытьФайлВТекущейВкладкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "RTF Files(*.rtf) | *.rtf";
            if (op.FileName == null) return;
            if (op.ShowDialog() == DialogResult.OK)
            {
                tabControl1.SelectedTab.Text = Path.GetFileName(op.FileName);
                TabPage p = tabControl1.SelectedTab;
                RichTextBox richtextbox = p.Controls.OfType<RichTextBox>().FirstOrDefault();
                selectedrtb = richtextbox;
                richtextbox.LoadFile(op.FileName, RichTextBoxStreamType.RichText);
            }
        }

        public void открытьФайлВНовойВкладкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "RTF Files(*.rtf) | *.rtf";
            op.ShowDialog();
            if (op.FileName == null) return;
            try
            {
                TabPage p = new TabPage(Path.GetFileName(op.FileName));
                tabControl1.TabPages.Add(p);
                RichTextBox richtextbox = new RichTextBox();
                p.Controls.Add(richtextbox);
                selectedrtb = richtextbox;
                richtextbox.Location = new Point(100, 100);
                richtextbox.Visible = true;
                richtextbox.Multiline = true;
                richtextbox.Dock = DockStyle.Fill;
                richtextbox.ContextMenuStrip = contextMenuStrip1;
                richtextbox.LoadFile(op.FileName);
            }
            catch (Exception Unknow_error)
            {
                MessageBox.Show(Unknow_error.Message, "Неизвестная ошибка", MessageBoxButtons.OK);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Заметка 1" || tabControl1.SelectedTab.Text == "Новая заметка")
            {
                TabPage p = tabControl1.SelectedTab;
                RichTextBox richtextbox = p.Controls.OfType<RichTextBox>().FirstOrDefault();
                selectedrtb = richtextbox;
                Form3 form3 = new Form3();
                form3.Show();
            }
            else 
            {
                TabPage p = tabControl1.SelectedTab;
                RichTextBox richtextbox = p.Controls.OfType<RichTextBox>().FirstOrDefault();
                selectedrtb = richtextbox;
                richtextbox.SaveFile(System.Windows.Forms.Application.StartupPath + "\\Files\\" + tabControl1.SelectedTab.Text);
                Form1.selectedrtb = richtextbox;
            }
        }

        private void списокЗаписокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl = tabControl1;
            Form4 form4 = new Form4();
            form4.Show();

        }
    }
}
