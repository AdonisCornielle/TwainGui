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
using System.Drawing;
using System.Drawing.Drawing2D;

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
            
            var paths = new List<string>();

            foreach (Image img in list)
            {
               
                string fileName = "";
                    
                fileName = txtBatchName.Text;
                string path = Path.Combine(folder, String.Format(fileName + "{0}.tiff", i));
                img.Save(path);
                paths.Add(path);
                i++;
            }

            DataGridView grid = nessScanning1.DataGridView1;

            string nombre = txtBatchName.Text;
            
            string ruta = Path.Combine(txtPath.Text, nombre + ".txt");
            StreamWriter writer = new StreamWriter(ruta);

            for (int row = 0; row < grid.Rows.Count; row++)
            {
                writer.WriteLine("Begin:");
                for (int col = 0; col < grid.Columns.Count; col++)
                {

                    if (grid.Rows[row].Cells[col].Value != null && col > 0)
                    {
                        
                        writer.WriteLine("Keyword" + col + ":" + grid.Rows[row].Cells[col].Value.ToString());
                    }

                    

                }
                //writer.WriteLine("Path:" + ruta.ToString() + i);
                writer.WriteLine("Path:" + paths[row]);
                i++;
                writer.WriteLine();
            }

            writer.Close();
            MessageBox.Show("Data Exportada");

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
                button5.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            
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

        private void button5_Click(object sender, EventArgs e)
        {
            nessScanning1.borrarItem();
            nessScanning1.borrarRow();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            openFileDialog.Filter = "Archivos de imagenes(*.tiff)|*.tiff|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {

                string FileName = openFileDialog.FileName;

                foreach (var f in openFileDialog.FileNames)
                {

                    //nessScanning1.ImagesList.Add(f);

                   
                }




            }
        }
    }
}