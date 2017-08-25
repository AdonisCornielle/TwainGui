using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ScanninControl
{
    public class WiaScannerException : ScannerException
    {
        private string m_WiaVersion;

        public WiaScannerException()
            : base()
        { }


        public WiaScannerException(Exception innerException)
            : base("Scanner Error", innerException)
        { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("WIA Exception");
            sb.AppendFormat("\nError Code: {0} - {1}", m_ErrorCode, (WiaScannerError)m_ErrorCode);
            sb.AppendFormat("\nWIA Version: {0}", m_WiaVersion);

            sb.AppendFormat("\n{0}", m_ScannerDeviceData.ToString());

            sb.AppendFormat("\n{0}", base.ToString());
            return sb.ToString();
        }

        #region Properties
        public string WiaVersion
        {
            get { return m_WiaVersion; }
            set { m_WiaVersion = value; }
        }
        #endregion
    }
}
