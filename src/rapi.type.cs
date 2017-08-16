using System;
using System.Runtime.InteropServices;

namespace CSRAPI
{
  [StructLayout(LayoutKind.Sequential)]
  public struct RAPIINIT
  {
    public int    cbSize;
    public IntPtr heRapiInit;
    public UInt32 hrRapiInit;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct CEOSVERSIONINFO
  {
    public UInt32 dwOSVersionInfoSize;
    public UInt32 dwMajorVersion;
    public UInt32 dwMinorVersion;
    public UInt32 dwBuildNumber;
    public UInt32 dwPlatformId;
    [MarshalAs(UnmanagedType.ByValTStr,SizeConst=128)]
    public string szCSDVersion;
  }

  public enum DesiredAccess : uint {
    GENERIC_READ    = 0x80000000,
    GENERIC_WRITE   = 0x40000000,
    GENERIC_EXECUTE = 0x20000000,
  }

  public enum ShareMode : int {
    FILE_SHARE_READ   = 0x00000001,
    FILE_SHARE_WRITE  = 0x00000002,
    FILE_SHARE_DELETE = 0x00000004,
  }

  public enum CreationDisposition : int {
    CREATE_NEW        = 1,
    CREATE_ALWAYS     = 2,
    OPEN_EXISTING     = 3,
    OPEN_ALWAYS       = 4,
    TRUNCATE_EXISTING = 5
  }

  public enum FlagsAndAttributes : int {
    FILE_ATTRIBUTE_ARCHIVE             = 0x00000020,
    FILE_ATTRIBUTE_ENCRYPTED           = 0x00004000,
    FILE_ATTRIBUTE_HIDDEN              = 0x00000002,
    FILE_ATTRIBUTE_NORMAL              = 0x00000080,
    FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
    FILE_ATTRIBUTE_OFFLINE             = 0x00001000,
    FILE_ATTRIBUTE_READONLY            = 0x00000001,
    FILE_ATTRIBUTE_SYSTEM              = 0x00000004,
    FILE_ATTRIBUTE_TEMPORARY           = 0x00000100,
  }
}
