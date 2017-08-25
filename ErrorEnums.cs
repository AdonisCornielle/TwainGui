using System;
using System.Collections.Generic;
using System.Text;

namespace ScanninControl
{
    public enum WiaScannerError : uint
    {
        Other = 0x00000000,
        LibraryNotInstalled = 0x80040154,
        OutputFileExists = 0x80070050,
        ScannerNotAvailable = 0x80210015,
        OperationCancelled = 0x80210064,
        PaperEmpty = 0x80210003,
        PaperJam = 0x80210002
    }

    public enum TwainScannerError : uint
    {
        Other = 0x00000000,
        ScannerNotAvailable = 0x00000001
    }
}
