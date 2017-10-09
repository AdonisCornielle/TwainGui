using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ScanninControl
{
    public class ScannerAdapterFactory
    {
        private static ScannerAdapterBase m_ScannerAdapterBase = null;
        private static object locker = new object();

        private ScannerAdapterFactory() { }

        public static ScannerAdapterBase GetScannerAdapter(nessScanning control, IMessageFilter messageFilter, IntPtr handle)
        {
            lock (locker)
            {
                bool isWiaDeviceSelected = false;
                //bool isTwainDeviceSelected = false;

                if (m_ScannerAdapterBase != null)
                {
                    return m_ScannerAdapterBase;
                }

                try
                {
                    //Checks WIA Devices
                    m_ScannerAdapterBase = new WiaScannerAdapter();
                    m_ScannerAdapterBase.InitAdapter(control, messageFilter, handle);
                    isWiaDeviceSelected = m_ScannerAdapterBase.SelectDevice();
                    if (isWiaDeviceSelected)
                    {
                        return m_ScannerAdapterBase;
                    }

                    ////Checks TWAIN Devices
                    //m_ScannerAdapterBase = new TwainScannerAdapter();
                    //m_ScannerAdapterBase.InitAdapter(control, messageFilter, handle);
                    //isTwainDeviceSelected = m_ScannerAdapterBase.SelectDevice();
                    //if (isTwainDeviceSelected)
                    //{
                    //    return m_ScannerAdapterBase;
                    //}
                }
                catch (ScannerException ex)
                {
                    throw ex;
                }

                return null;
            }
        }
    }
}
