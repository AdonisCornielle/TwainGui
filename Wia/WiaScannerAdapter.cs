using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using WIA;
using System.Threading;

namespace ScanninControl
{
    public class WiaScannerAdapter : ScannerAdapterBase
    {
        #region Private Classes
        class WIA_DPS_DOCUMENT_HANDLING_SELECT
        {
            public const uint FEEDER = 0x00000001;
            public const uint FLATBED = 0x00000002;
        }

        class WIA_DPS_DOCUMENT_HANDLING_STATUS
        {
            public const uint FEED_READY = 0x00000001;
        }

        class WIA_PROPERTIES
        {
            public const uint WIA_RESERVED_FOR_NEW_PROPS = 1024;
            public const uint WIA_DIP_FIRST = 2;
            public const uint WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            public const uint WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            //
            // Scanner only device properties (DPS)
            //
            public const uint WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            public const uint WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13;
            public const uint WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14;
        }

        class WIA_DEVICE_PROPERTIES
        {
            public const string WIA_DEVICE_MANUFACTURER = "Manufacturer";
            public const string WIA_DEVICE_DESCRIPTION = "Description";
            public const string WIA_DEVICE_NAME = "Name";
            public const string WIA_DEVICE_WIA_VERSION = "WIA Version";
            public const string WIA_DEVICE_DRIVER_VERSIONR = "Driver Version";
        }

        class WIA_ERRORS
        {
            public const uint BASE_VAL_WIA_ERROR = 0x80210000;
            public const uint WIA_ERROR_PAPER_EMPTY = BASE_VAL_WIA_ERROR + 3;
        }
        #endregion

        private string m_DeviceID = null;
        private string m_WiaVersion;

        public WiaScannerAdapter()
        { }

        public override void InitAdapter(nessScanning control, System.Windows.Forms.IMessageFilter messageFilter, IntPtr handle)
        { }

        public override bool SelectDevice()
        {
            CommonDialogClass wiaCommonDialog = new CommonDialogClass();
            Device device = null;
            try
            {
                device = wiaCommonDialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, true);
                if (device != null)
                {
                    m_DeviceID = device.DeviceID;
                    FillDeviceData(device);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (COMException ex)
            {
                if ((WiaScannerError)ex.ErrorCode == WiaScannerError.ScannerNotAvailable)
                {
                    return false;
                }
                else
                {
                    WiaScannerException se = BuildScannerException(device, ex);
                    throw se;
                }
            }
        }

        public override void AcquireImages(bool showUI)
        {
            CommonDialogClass wiaCommonDialog = new CommonDialogClass();

            if (m_DeviceID == null)
            {
                SelectDevice();
            }

            //Create DeviceManager
            Device WiaDev = CreateDeviceManager();

            WIA.Item scanningItem = GetScanningProperties(showUI, wiaCommonDialog, WiaDev);
            if (scanningItem == null)
            {
                return;
            }

            WIA.ImageFile imgFile = null;
            WIA.Item item = null;

            PrepareImagesList();

            //Start Scan
            while (HasMorePages(WiaDev))
            {
                item = scanningItem;

                try
                {
                    imgFile = ScanImage(wiaCommonDialog, imgFile, item);

                }
                catch (COMException ex)
                {
                    if ((WiaScannerError)ex.ErrorCode == WiaScannerError.PaperEmpty)
                    {
                        break;
                    }
                    else
                    {
                        WiaScannerException se = BuildScannerException(WiaDev, ex);
                        throw se;
                    }
                }
                catch (Exception ex)
                {
                    WiaScannerException se = BuildScannerException(WiaDev, ex);
                    throw se;
                }
                finally
                {
                    item = null;
                }
            }
        }

        #region Private Members
        private WiaScannerException BuildScannerException(Device WiaDev, Exception exception)
        {
            WiaScannerException scannerException = new WiaScannerException(exception);

            if (exception is COMException)
            {
                scannerException.ErrorCode = (exception as COMException).ErrorCode;
            }

            if (m_ScannerDeviceData != null)
            {
                scannerException.ScannerDeviceData = m_ScannerDeviceData;
                scannerException.WiaVersion = m_WiaVersion;
            }
            return scannerException;
        }

        private void FillDeviceData(Device WiaDev)
        {
            m_ScannerDeviceData = new ScannerDeviceData();
            if (WiaDev != null)
            {
                foreach (WIA.Property prop in WiaDev.Properties)
                {
                    switch (prop.Name)
                    {
                        case WIA_DEVICE_PROPERTIES.WIA_DEVICE_MANUFACTURER:
                            m_ScannerDeviceData.DeviceManufacturer = prop.get_Value().ToString();
                            break;
                        case WIA_DEVICE_PROPERTIES.WIA_DEVICE_DESCRIPTION:
                            m_ScannerDeviceData.DeviceDescription = prop.get_Value().ToString();
                            break;
                        case WIA_DEVICE_PROPERTIES.WIA_DEVICE_NAME:
                            m_ScannerDeviceData.DeviceName = prop.get_Value().ToString();
                            break;
                        case WIA_DEVICE_PROPERTIES.WIA_DEVICE_DRIVER_VERSIONR:
                            m_ScannerDeviceData.DeviceDriverVersion = prop.get_Value().ToString();
                            break;
                        case WIA_DEVICE_PROPERTIES.WIA_DEVICE_WIA_VERSION:
                            m_WiaVersion = prop.get_Value().ToString();
                            break;
                    }
                }
            }
        }

        private Device CreateDeviceManager()
        {
            DeviceManager manager = new DeviceManagerClass();
            Device wiaDev = null;
            foreach (DeviceInfo info in manager.DeviceInfos)
            {
                if (info.DeviceID == m_DeviceID)
                {
                    WIA.Properties infoprop = null;
                    infoprop = info.Properties;

                    //connect to scanner
                    try
                    {
                        Thread.Sleep(500);
                        wiaDev = info.Connect();

                    }
                    catch (Exception)
                    {
                        Thread.Sleep(500);
                        //throw;
                    }
                    
                    break;
                }
            }
            return wiaDev;
        }

        private WIA.Item GetScanningProperties(bool showUI, CommonDialogClass wiaCommonDialog, Device WiaDev)
        {
            WIA.Items items = null;
            if (showUI)
            {
                try
                {
                    items = wiaCommonDialog.ShowSelectItems(WiaDev, WiaImageIntent.TextIntent, WiaImageBias.MinimizeSize, false, true, false);

                }
                catch (Exception)
                {
                    
                }
            }
            else
            {
                items = WiaDev.Items;
            }

            WIA.Item scanningItem = null;
            if (items != null && items.Count > 0)
            {
                scanningItem = items[1] as WIA.Item;
            }
            return scanningItem;
        }

        private WIA.ImageFile ScanImage(CommonDialogClass wiaCommonDialog, WIA.ImageFile imgFile, WIA.Item item)
        {
            imgFile = (ImageFile)wiaCommonDialog.ShowTransfer(item, ImageFormat.Tiff.Guid.ToString("B")/* wiaFormatTiff*/, false);

            byte[] buffer = (byte[])imgFile.FileData.get_BinaryData();
            MemoryStream ms = new MemoryStream(buffer);
            m_ImagesList.Add(Image.FromStream(ms));

            imgFile = null;
            return imgFile;
        }

        private bool HasMorePages(Device WiaDev)
        {
            try
            {
                bool hasMorePages = false;
                //determina si hay mas de una pagina en espera
                Property documentHandlingSelect = null;
                Property documentHandlingStatus = null;
                foreach (Property prop in WiaDev.Properties)
                {
                    if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT)
                        documentHandlingSelect = prop;
                    if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                        documentHandlingStatus = prop;
                }

                if (documentHandlingSelect != null) //may not exist on flatbed scanner but required for feeder
                {
                    //check for document feeder
                    if ((Convert.ToUInt32(documentHandlingSelect.get_Value()) & WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER) != 0)
                    {
                        hasMorePages = ((Convert.ToUInt32(documentHandlingStatus.get_Value()) & WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) != 0);
                    }
                }
                return hasMorePages;
            }
            catch (COMException ex)
            {
                WiaScannerException se = BuildScannerException(WiaDev, ex);
                throw se;
            }
        }
        #endregion
    }
}