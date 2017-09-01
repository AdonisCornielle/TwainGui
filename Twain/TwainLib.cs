using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace ScanninControl
{
    public class Twain
    {
        #region Private Members
        private const short C_COUNTRY_USA = 1;
        private const short C_LANGUAGE_USA = 13;

        private IntPtr hwnd;
        private TwIdentity appid;
        private TwIdentity srcds;
        private TwEvent evtmsg;
        private WINMSG winmsg;

        private ScannerDeviceData m_TwainDevice = null;
        #endregion

        public Twain()
        {
            appid = new TwIdentity();
            appid.Id = IntPtr.Zero;
            appid.Version.MajorNum = 1;
            appid.Version.MinorNum = 1;
            appid.Version.Language = C_LANGUAGE_USA;
            appid.Version.Country = C_COUNTRY_USA;
            appid.Version.Info = "Hack 1";
            appid.ProtocolMajor = TwProtocol.Major;
            appid.ProtocolMinor = TwProtocol.Minor;
            appid.SupportedGroups = (int)(TwDG.Image | TwDG.Control);
            appid.Manufacturer = "NETMaster";
            appid.ProductFamily = "Freeware";
            appid.ProductName = "Hack";

            srcds = new TwIdentity();
            srcds.Id = IntPtr.Zero;

            evtmsg.EventPtr = Marshal.AllocHGlobal(Marshal.SizeOf(winmsg));
        }

        ~Twain()
        {
            Marshal.FreeHGlobal(evtmsg.EventPtr);
        }

        public void Init(IntPtr hwndp)
        {
            
            try
            {
                Finish();
                TwRC rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.OpenDSM, ref hwndp);
                if (rc == TwRC.Success)
                {
                    rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, srcds);
                    if (rc == TwRC.Success)
                    {
                        hwnd = hwndp;
                    }
                    else
                    {
                        rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwndp);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: Conecte el Dispositivo de Escaneo. \n" + ex.Message);
            }
            
        }

        public bool Select()
        {
            TwRC rc;
            CloseSrc();
            if (appid.Id == IntPtr.Zero)
            {
                Init(hwnd);
                if (appid.Id == IntPtr.Zero)
                {
                    return false;
                }
            }
            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.UserSelect, srcds);
            FillDeviceData(srcds);
            return true;
        }

        public void Acquire(bool showUI)
        {
            TwRC rc;
            CloseSrc();
            if (appid.Id == IntPtr.Zero)
            {
                Init(hwnd);
                if (appid.Id == IntPtr.Zero)
                {
                    BuildScannerException(srcds, TwainScannerError.ScannerNotAvailable);
                }
            }
            
            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds);
            if (rc != TwRC.Success)
            {
                BuildScannerException(srcds, TwainScannerError.ScannerNotAvailable);
            }

            TwCapability cap = new TwCapability(TwCap.XferCount, 1);
            rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                BuildScannerException(srcds, TwainScannerError.Other);
            }

            TwUserInterface guif = new TwUserInterface();
            guif.ShowUI = Convert.ToInt16(showUI);
            guif.ModalUI = 1;
            guif.ParentHand = hwnd;
            rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
            if (rc != TwRC.Success)
            {
                CloseSrc();
                BuildScannerException(srcds, TwainScannerError.Other);
            }
        }

        public ArrayList TransferPictures()
        {
            ArrayList pics = new ArrayList();
            if (srcds.Id == IntPtr.Zero)
            {
                return pics;
            }

            TwRC rc;
            IntPtr hbitmap = IntPtr.Zero;
            TwPendingXfers pxfr = new TwPendingXfers();

            do
            {
                pxfr.Count = 0;
                hbitmap = IntPtr.Zero;

                TwImageInfo iinf = new TwImageInfo();
                rc = DSiinf(appid, srcds, TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, iinf);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return pics;
                }

                rc = DSixfer(appid, srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref hbitmap);
                if (rc != TwRC.XferDone)
                {
                    CloseSrc();
                    return pics;
                }

                rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return pics;
                }

                pics.Add(hbitmap);
            }
            while (pxfr.Count != 0);

            rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr);
            return pics;
        }

        public TwainCommand PassMessage(ref Message m)
        {
            if (srcds.Id == IntPtr.Zero)
            {
                return TwainCommand.Not;
            }

            int pos = GetMessagePos();

            winmsg.hwnd = m.HWnd;
            winmsg.message = m.Msg;
            winmsg.wParam = m.WParam;
            winmsg.lParam = m.LParam;
            winmsg.time = GetMessageTime();
            winmsg.x = (short)pos;
            winmsg.y = (short)(pos >> 16);

            Marshal.StructureToPtr(winmsg, evtmsg.EventPtr, false);
            evtmsg.Message = 0;
            TwRC rc = DSevent(appid, srcds, TwDG.Control, TwDAT.Event, TwMSG.ProcessEvent, ref evtmsg);
            if (rc == TwRC.NotDSEvent)
            {
                return TwainCommand.Not;
            }
            if (evtmsg.Message == (short)TwMSG.XFerReady)
            {
                return TwainCommand.TransferReady;
            }
            if (evtmsg.Message == (short)TwMSG.CloseDSReq)
            {
                return TwainCommand.CloseRequest;
            }
            if (evtmsg.Message == (short)TwMSG.CloseDSOK)
            {
                return TwainCommand.CloseOk;
            }
            if (evtmsg.Message == (short)TwMSG.DeviceEvent)
            {
                return TwainCommand.DeviceEvent;
            }
            return TwainCommand.Null;
        }

        public void CloseSrc()
        {
            TwRC rc;
            if (srcds.Id != IntPtr.Zero)
            {
                TwUserInterface guif = new TwUserInterface();
                rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif);
                rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, srcds);
            }
        }

        public void Finish()
        {
            TwRC rc;
            CloseSrc();
            if (appid.Id != IntPtr.Zero)
            {
                rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwnd);
            }
            appid.Id = IntPtr.Zero;
        }

        public Image ImageFromIntPtr(IntPtr imagePtr)
        {
            BITMAPINFOHEADER bmpInfoHeader = new BITMAPINFOHEADER();
            IntPtr bmpptr = GlobalLock(imagePtr);
            IntPtr pixptr = GetPixelInfo(bmpptr, bmpInfoHeader);

            Bitmap bitmap = new Bitmap(bmpInfoHeader.biWidth, bmpInfoHeader.biHeight);
            Graphics graphics = Graphics.FromImage(bitmap);

            IntPtr hdc = graphics.GetHdc();
            SetDIBitsToDevice(hdc, 0, 0, bmpInfoHeader.biWidth, bmpInfoHeader.biHeight,
                    0, 0, 0, bmpInfoHeader.biHeight, pixptr, bmpptr, 0);
            graphics.ReleaseHdc(hdc);

            return (Image)bitmap;
        }

        #region Properties
        public ScannerDeviceData TwainDevice
        {
            get { return m_TwainDevice; }
            set { m_TwainDevice = value; }
        }

        public static int ScreenBitDepth
        {
            get
            {
                IntPtr screenDC = CreateDC("DISPLAY", null, null, IntPtr.Zero);
                int bitDepth = GetDeviceCaps(screenDC, 12);
                bitDepth *= GetDeviceCaps(screenDC, 14);
                DeleteDC(screenDC);
                return bitDepth;
            }
        }
        #endregion

        #region Private Methods
        private void FillDeviceData(TwIdentity srcds)
        {
            m_TwainDevice = new ScannerDeviceData();
            if (srcds != null && srcds.Id != IntPtr.Zero)
            {
                m_TwainDevice.DeviceManufacturer = srcds.Manufacturer;
                m_TwainDevice.DeviceName = srcds.ProductName;
                m_TwainDevice.DeviceFamily = srcds.ProductFamily;
                m_TwainDevice.DeviceDriverVersion = srcds.Version.Info;
            }
        }

        private TwainScannerException BuildScannerException(TwIdentity srcds, TwainScannerError twainScannerError)
        {
            TwainScannerException scannerException = new TwainScannerException();
            scannerException.ErrorCode = Convert.ToInt32(twainScannerError);

            if (m_TwainDevice != null)
            {
                scannerException.ScannerDeviceData = m_TwainDevice;
            }

            return scannerException;
        }

        private IntPtr GetPixelInfo(IntPtr bmpptr, BITMAPINFOHEADER bmpInfoHeader)
        {
            Marshal.PtrToStructure(bmpptr, bmpInfoHeader);

            if (bmpInfoHeader.biSizeImage == 0)
                bmpInfoHeader.biSizeImage = ((((bmpInfoHeader.biWidth * bmpInfoHeader.biBitCount) + 31) & ~31) >> 3) * bmpInfoHeader.biHeight;

            int p = bmpInfoHeader.biClrUsed;
            if ((p == 0) && (bmpInfoHeader.biBitCount <= 8))
                p = 1 << bmpInfoHeader.biBitCount;
            p = (p * 4) + bmpInfoHeader.biSize + (int)bmpptr;
            return (IntPtr)p;
        }
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        internal struct WINMSG
        {
            public IntPtr hwnd;
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            public int time;
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        internal class BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        }
        #endregion

        #region External DLLs Import
        // ------ DSM entry point DAT_ variants:
        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSMparent([In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr refptr);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSMident([In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity idds);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSMstatus([In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat);

        // ------ DSM entry point DAT_ variants to DS:
        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSuserif([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface guif);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSevent([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent evt);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSstatus([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DScap([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability capa);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSiinf([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwImageInfo imginf);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSixfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSpxfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pxfr);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalAlloc(int flags, int size);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern bool GlobalUnlock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int GetMessagePos();

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int GetMessageTime();


        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateDC(string szdriver, string szdevice, string szoutput, IntPtr devmode);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern int SetDIBitsToDevice(IntPtr hdc, int xdst, int ydst,
                                                int width, int height, int xsrc, int ysrc, int start, int lines,
                                                IntPtr bitsptr, IntPtr bmiptr, int color);
        #endregion
    } // class Twain
}
