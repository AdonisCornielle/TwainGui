using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ScanninControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            nessScanning1.AcquirePictures();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Image> list = nessScanning1.ImagesList;
            int i = 1;
            string folder = txtPath.Text;

            foreach (Image img in list)
            {
               
                string fileName = "";
                    
                fileName = txtBatchName.Text;
                string path = Path.Combine(folder, String.Format(fileName + " {0}.jpg", i));
                img.Save(path);
                i++;
            }
        }

     

        private void button3_Click(object sender, EventArgs e)
        {
            nessScanning1.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            nessScanning1.ShowUI = true;
            bool initSuccess = nessScanning1.InitScanner();
            if (!initSuccess)
            {
                MessageBox.Show("No Scanner Selected");
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = folderDlg.SelectedPath;
            }
        }

        private void nessScanning1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtBatchName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}