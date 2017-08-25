using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace ScanninControl
{
    public class TwainScannerAdapter : ScannerAdapterBase
    {
        private Twain m_Twain = null;
        private bool m_MsgFilter;
        private IMessageFilter m_MessageFilter = null;
        private nessScanning m_Control = null;

        public TwainScannerAdapter()
        {
            m_Twain = new Twain();
        }

        public override void InitAdapter(nessScanning control, IMessageFilter messageFilter, IntPtr handle)
        {
            m_Control = control;
            m_MessageFilter = messageFilter;
            m_Twain.Init(handle);
            m_ScannerDeviceData = m_Twain.TwainDevice;
        }

        public override bool SelectDevice()
        {
            bool returnVal = m_Twain.Select();
            m_ScannerDeviceData = m_Twain.TwainDevice;

            return returnVal;
        }

        public override void AcquireImages(bool showUI)
        {
            if (!m_MsgFilter)
            {
                m_Control.Enabled = false;
                m_MsgFilter = true;
                Application.AddMessageFilter(m_MessageFilter);
            }
            try
            {
                m_Twain.Acquire(showUI);
            }
            catch (TwainScannerException ex)
            {
                throw ex;
            }
        }

        public bool PreFilterMessage(ref Message message)
        {
            TwainCommand cmd = m_Twain.PassMessage(ref message);
            if (cmd == TwainCommand.Not)
                return false;

            switch (cmd)
            {
                case TwainCommand.CloseRequest:
                    {
                        EndingScan();
                        m_Twain.CloseSrc();
                        break;
                    }
                case TwainCommand.CloseOk:
                    {
                        EndingScan();
                        m_Twain.CloseSrc();
                        break;
                    }
                case TwainCommand.DeviceEvent:
                    {
                        break;
                    }
                case TwainCommand.TransferReady:
                    {
                        TransferPictures();
                        break;
                    }
            }
            return true;
        }

        #region Private Members
        private void TransferPictures()
        {
            try
            {
                ArrayList m_Pictures = m_Twain.TransferPictures();
                EndingScan();
                m_Twain.CloseSrc();
                if (m_Pictures.Count > 0)
                {
                    PrepareImagesList();
                    for (int i = 0; i < m_Pictures.Count; i++)
                    {
                        m_ImagesList.Add(m_Twain.ImageFromIntPtr((IntPtr)m_Pictures[i]));
                    }
                    m_Control.Controller.LoadThumbs();
                }
                else
                {
                    m_ImagesList = null;
                }
            }
            catch (TwainScannerException ex)
            {
                throw ex;
            }
        }

        private void EndingScan()
        {
            if (m_MsgFilter)
            {
                Application.RemoveMessageFilter(m_MessageFilter);
                m_MsgFilter = false;
                m_Control.Enabled = true;
            }
        }
        #endregion
    }
}
