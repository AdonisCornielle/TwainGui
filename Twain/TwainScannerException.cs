using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ScanninControl
{
    public class TwainScannerException : ScannerException
    {
        public TwainScannerException()
            : base()
        { }

        public TwainScannerException(Exception innerException)
            : base("Scanner Error", innerException)
        { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("TWAIN Exception");
            sb.AppendFormat("\nError Code: {0} - {1}", m_ErrorCode, (TwainScannerError)m_ErrorCode);

            sb.AppendFormat("\n{0}", m_ScannerDeviceData.ToString());

            sb.AppendFormat("\n{0}", base.ToString());
            return sb.ToString();
        }
    }
}
