using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ScanninControl
{
    public class ScannerException : ApplicationException
    {
        protected ScannerDeviceData m_ScannerDeviceData;
        protected int m_ErrorCode;

        public ScannerException()
            : base()
        { }


        public ScannerException(Exception innerException)
            : this("Scanner Error", innerException)
        { }

        public ScannerException(string message, Exception innerException)
            : base("Scanner Error", innerException)
        { }

        #region Properties
        public ScannerDeviceData ScannerDeviceData
        {
            get { return m_ScannerDeviceData; }
            set { m_ScannerDeviceData = value; }
        }
        public int ErrorCode
        {
            get { return m_ErrorCode; }
            set { m_ErrorCode = value; }
        }
        #endregion
    }
}
