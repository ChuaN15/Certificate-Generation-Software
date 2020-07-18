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

namespace WSCTraining_German3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Effect == DragDropEffects.Copy)
            {
                string[] strings = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                string daata = "";
                for (int i = 0; i < strings.Count(); i++)
                {
                    daata += strings[i];
                }
                MessageBox.Show(daata);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            
            if(folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string[] files = Directory.GetFiles(folderBrowser.SelectedPath,"*.txt",SearchOption.AllDirectories);

                string daata = "";
                for (int i = 0; i < files.Count(); i++)
                {
                    daata += files[i];
                }
                MessageBox.Show(daata);
            }
        }
    }
}
