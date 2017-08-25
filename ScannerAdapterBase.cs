using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ScanninControl
{
    public abstract class ScannerAdapterBase
    {
        protected List<Image> m_ImagesList = null;
        protected ScannerDeviceData m_ScannerDeviceData = null;

        /// <summary>
        /// Initializes the Adapter
        /// </summary>
        public abstract void InitAdapter(nessScanning control, IMessageFilter messageFilter, IntPtr handle);

        /// <summary>
        /// Selects scanning device, and returns the indicator that the device selected
        /// </summary>
        /// <returns>the indicator that the device selected</returns>
        public abstract bool SelectDevice();

        /// <summary>
        /// Acquires images from scanning device and fills ImagesLis
        /// </summary>
        public abstract void AcquireImages(bool showUI);

        public void CleanImagesList()
        {
            if (m_ImagesList != null)
            {
                m_ImagesList.Clear();
            }
        }

        public List<Image> ImagesList
        {
            get { return m_ImagesList; }
        }

        public ScannerDeviceData ScannerDeviceData
        {
            get { return m_ScannerDeviceData; }
            set { m_ScannerDeviceData = value; }
        }

        protected void PrepareImagesList()
        {
            if (m_ImagesList == null)
            {
                m_ImagesList = new List<Image>();
            }
        }
    }
}
