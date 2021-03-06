using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ScanninControl
{
    public partial class nessScanning : UserControl, IMessageFilter
    {

        private nessScanningCA m_Controller = null;
        private bool m_ShowUI = true;

        public nessScanning()
        {
            InitializeComponent();
        }

        public bool InitScanner()
        {
            bool retVal = false;

            m_Controller = new nessScanningCA();
            m_Controller.Control = this;
            retVal = m_Controller.InitScanner();
            m_Controller.InitEvents();

            return retVal;
        }

        public void Clear()
        {
            m_Controller.Clear();
        }

        public void borrarItem()
        {
            m_Controller.borrarItem();
        }

        public void borrarRow()
        {
            m_Controller.borrarRow();
        }

        public void exportarTXT()
        {
            m_Controller.exportarTXT();
        }

        public void loadIndexa()
        {
            m_Controller.loadIndexa();

        } 

        public void SelectDevice()
        {
            m_Controller.SelectDevice();
        }

       
        public void AcquirePictures()
        {
            m_Controller.AcquireImages();
        }

        bool IMessageFilter.PreFilterMessage(ref Message message)
        {
            return m_Controller.PreFilterMessage(ref message);
        }

        #region Properties
        public nessScanningCA Controller
        {
            get { return m_Controller; }
        }
        public bool ShowUI
        {
            get { return m_ShowUI; }
            set { m_ShowUI = value; }
        }

        public List<Image> ImagesList
        {
            get { return m_Controller.ImagesList; }
        }

        public ImageList ImagesTumbList
        {
            get { return imagesTumbList; }
        }

        public ListView ImagesListView
        {
            get { return imagesListView; }
        }

        public PictureBox PreviewPictureBox
        {
            get { return previewPictureBox; }
        }

        public DataGridView DataGridView1
        {
            get { return dataGridView1; }
        }

        #endregion

        private void imagesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void previewPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
            for (int j = DataGridView1.SelectedRows.Count - 1; j >= 0; j--)
            {
                DataGridView1.Rows.RemoveAt(index: j);

            }
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void imagesListView_ItemActivate(object sender, EventArgs e)
        {
            
        }
    }
}