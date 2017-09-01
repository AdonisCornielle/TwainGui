using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ScanninControl
{
    public class nessScanningCA
    {
        #region Constants
        private const int C_THUMB_WIDTH = 71;
        private const int C_THUMB_HEIGTH = 100;
        #endregion

        #region Private Members
        private nessScanning m_Control = null;
        private ScannerAdapterBase m_ScannerAdapterBase = null;
        #endregion

        #region Properties
        public nessScanning Control
        {
            get { return m_Control; }
            set { m_Control = value; }
        }

        public List<Image> ImagesList
        {
            get { return m_ScannerAdapterBase.ImagesList; }
        }
        #endregion

        public nessScanningCA()
        {
        }

        #region Initialization
        public bool InitScanner()
        {
            try
            {
                m_ScannerAdapterBase = ScannerAdapterFactory.GetScannerAdapter(Control, Control, Control.Handle);
            }
            catch (ScannerException ex)
            {
                HandledException(ex);
                return false;
            }
            return (m_ScannerAdapterBase != null);
        }

        public void InitEvents()
        {
            Control.ImagesListView.SelectedIndexChanged += new EventHandler(ImagesListView_SelectedIndexChanged);
        }
        #endregion

        public void Clear()
        {
            if (m_ScannerAdapterBase.ImagesList != null)
            {
                ImagesList.Clear();
            }
            LoadThumbs();
        }

        public void AcquireImages()
        {
            if (m_ScannerAdapterBase != null)
            {
                try
                {
                    m_ScannerAdapterBase.AcquireImages(Control.ShowUI);
                }
                catch (ScannerException ex)
                {
                    HandledException(ex);
                }
                LoadThumbs();
            }
        }

        public void SelectDevice()
        {
            try
            {
                if (m_ScannerAdapterBase != null)
                {

                    m_ScannerAdapterBase.SelectDevice();
                }
                else
                {
                    InitScanner();
                }
            }
            catch (ScannerException ex)
            {
                HandledException(ex);
            }
        }

        public bool PreFilterMessage(ref Message message)
        {
            bool retVal = true;
            if (m_ScannerAdapterBase != null && m_ScannerAdapterBase is TwainScannerAdapter)
            {
                retVal = (m_ScannerAdapterBase as TwainScannerAdapter).PreFilterMessage(ref message);
            }
            return retVal;
        }

        #region Events
        private void ImagesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Control.ImagesListView.SelectedIndices.Count > 0)
            {
                int index = Control.ImagesListView.SelectedIndices[0];
                Image i = resizeImage(m_ScannerAdapterBase.ImagesList[index], new Size(Control.PreviewPictureBox.Width, Control.PreviewPictureBox.Height));

                Control.PreviewPictureBox.Image = i;
            }
            else
            {
                Control.PreviewPictureBox.Image = null;
            }
        }
        #endregion

        #region Private Methods
        public void LoadThumbs()
        {
            Control.ImagesTumbList.Images.Clear();
            Control.ImagesListView.Items.Clear();
            Control.PreviewPictureBox.Image = null;
            if (m_ScannerAdapterBase.ImagesList != null && m_ScannerAdapterBase.ImagesList.Count > 0)
            {
                Control.ImagesListView.View = View.LargeIcon;
                Control.ImagesTumbList.ImageSize = new Size(C_THUMB_WIDTH, C_THUMB_HEIGTH);
                for (int c = 0; c < m_ScannerAdapterBase.ImagesList.Count; c++)
                {
                    Image img = resizeImage(m_ScannerAdapterBase.ImagesList[c], new Size(C_THUMB_WIDTH, C_THUMB_HEIGTH));
                    Control.ImagesTumbList.Images.Add(img);
                }

                Control.ImagesListView.LargeImageList = Control.ImagesTumbList;

                for (int j = 0; j < Control.ImagesTumbList.Images.Count; j++)
                {
                    ListViewItem lstItem = new ListViewItem();
                    lstItem.ImageIndex = j;
                    lstItem.Text = string.Format("Pagina {0}", j + 1);
                    Control.ImagesListView.Items.Add(lstItem);
                }
            }
        }



        private Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }



        public void borrarItem()
        {
            //31/08/2017
            //Evalua si la item seleccionado en en listview es mayor que cero
            if (Control.ImagesListView.SelectedItems.Count > 0)
            {
                //MessageBox que para confirmar si desea eliminar la imagen
                var confirmation = MessageBox.Show("Desea eliminar la imagen Seleccionado?", "Eliminar Imagen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //Si la respuesta es correcta, entonces recorre el arreglo para eliminar el item
                if (confirmation == DialogResult.Yes)
                {

                    for (int i = Control.ImagesListView.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        //Almacena en la variable itm cual fue el item seleccionado
                        ListViewItem itm = Control.ImagesListView.SelectedItems[i];
                        //Elimina el thumbnail del listview
                        Control.ImagesListView.Items[itm.Index].Remove();
                        //Elimina la imagen seleccionada dentro del imagelist
                        Control.ImagesList.RemoveAt(index: i);
                    }
                }
                else
                    MessageBox.Show("Ningún Item Seleccionado");

            }
        }

        private void HandledException(ScannerException scannerException)
        {
            MessageBox.Show(scannerException.ToString());
        }
        #endregion
    }
}

