using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ScanninControl
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        public MDIParent1()
        {

            //Thread t = new Thread(new ThreadStart(StartForm));
            //t.Start();
            //Thread.Sleep();
            InitializeComponent();
            //t.Abort();

        }
        //public void StartForm()
        //{
        //    Application.Run(new FormIntroCSI());

        //}

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

     
        public void OpenFile(object sender, EventArgs e)
        {
        
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            
        }

       
        private void batchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.WindowsDefaultBounds;
            form1.ControlBox = true;
            form1.MinimizeBox = true;
            form1.MaximizeBox = true;
            form1.ShowInTaskbar = false;
            form1.Dock = DockStyle.Fill;
            form1.MdiParent = this;
            form1.Show();

            this.WindowState = FormWindowState.Maximized;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
           
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
