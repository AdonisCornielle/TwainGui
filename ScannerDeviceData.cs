using System;
using System.Collections.Generic;
using System.Text;

namespace ScanninControl
{
    public class ScannerDeviceData
    {
        private string m_DeviceManufacturer;
        private string m_DeviceName;
        private string m_DeviceFamily;
        private string m_DeviceDescription;
        private string m_DeviceDriverVersion;

        public ScannerDeviceData()
        { }

        public ScannerDeviceData(string deviceManufacturer, string deviceName, string deviceFamily,
                                string deviceDescription, string deviceDriverVersion)
        {
            m_DeviceManufacturer = deviceManufacturer;
            m_DeviceName = deviceName;
            m_DeviceFamily = deviceFamily;
            m_DeviceDescription = deviceDescription;
            m_DeviceDriverVersion = deviceDriverVersion;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Device Manufacturer: {0}", m_DeviceManufacturer);
            sb.AppendFormat("\nDevice Name: {0}", m_DeviceName);
            sb.AppendFormat("\nDevice Family: {0}", m_DeviceFamily);
            sb.AppendFormat("\nDescription: {0}", m_DeviceDescription);
            sb.AppendFormat("\nDriver Version: {0}", m_DeviceDriverVersion);

            return sb.ToString();
        }

        #region Properties
        public string DeviceManufacturer
        {
            get { return m_DeviceManufacturer; }
            set { m_DeviceManufacturer = value; }
        }
        public string DeviceName
        {
            get { return m_DeviceName; }
            set { m_DeviceName = value; }
        }
        public string DeviceFamily
        {
            get { return m_DeviceFamily; }
            set { m_DeviceFamily = value; }
        }
        public string DeviceDescription
        {
            get { return m_DeviceDescription; }
            set { m_DeviceDescription = value; }
        }
        public string DeviceDriverVersion
        {
            get { return m_DeviceDriverVersion; }
            set { m_DeviceDriverVersion = value; }
        }
        #endregion
    }
}
