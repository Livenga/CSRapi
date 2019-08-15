using System;
using System.Runtime.InteropServices;


namespace CSRAPI {
  public class Defines {
    public static int CEDB_MAXDBASENAMELEN = 32;
    public static int CEDB_MAXSORTORDER    = 4;
  }


  [StructLayout(LayoutKind.Sequential)]
  public struct RAPIINIT {
    public int    cbSize;
    public IntPtr heRapiInit;
    public UInt32 hrRapiInit;
  }


  [StructLayout(LayoutKind.Sequential)]
  public struct CEOSVERSIONINFO {
    public UInt32 dwOSVersionInfoSize;
    public UInt32 dwMajorVersion;
    public UInt32 dwMinorVersion;
    public UInt32 dwBuildNumber;
    public UInt32 dwPlatformId;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string szCSDVersion;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct SystemPowerStatusEx {
    public byte  ACLineStauts;
    public byte  BatteryFlag;
    public byte  BatteryLifePercent;
    public byte  Reserved1;
    public ulong BatteryLifeTime;
    public ulong BatteryFullLifeTime;
    public byte  Reserved2;
    public byte  BackupBatteryFlag;
    public byte  BackupBatteryLifePercent;
    public byte  Reserved3;
    public ulong BackupBaterryLifeTime;
    public ulong BackupBaterryFullLifeTime;

  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct MemoryStatus {
    public ulong dwLength;
    public ulong dwMemoryLoad;
    public ulong dwTotalPhys;
    public ulong dwAvailPhys;
    public ulong dwTotalPageFile;
    public ulong dwAvailPageFile;
    public ulong dwTotalVirtual;
    public ulong dwAvailVirtual;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct StoreInformation {
    public UInt32 dwStoreSize;
    public UInt32 dwFreeSize;
  }
 
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct FileTime {
    public UInt32 dwLowDateTime;
    public UInt32 dwHighDateTime;
  };

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct CeFindData {
    public UInt32   dwFileAttributes;
    public FileTime ftCreationTime;
    public FileTime ftLastAccessTime;
    public FileTime ftLastWriteTime;
    public UInt32   nFileSizeHigh;
    public UInt32   nFileSizeLow;
    public UInt32   dwOID;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string   cFileName;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct ProcessInformation {
    public IntPtr hProcess;
    public IntPtr hThread;
    public UInt32 dwProcessId;
    public UInt32 dwThreadId;
  }

  public struct SortOrderSpec {
    public ulong /*CePropId*/ propid;
    public ulong dwFlags;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct CeFileInfo {
    public ulong    dwAttributes;
    public ulong    oidParent;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string   szFileName;
    public FileTime ftLastChanged;
    public ulong    dwLength;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct CeDirInfo {
    public ulong  dwAttributes;
    public ulong  oidParent;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string szDirName;  
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct CeDBaseInfo {
    public ulong           dwFlags;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string          szDbaseName;
    public ulong           dwDbaseType;
    public int             dwNumRecords;
    public int             dwNumSortOrder;
    public ulong           dwSize;
    public FileTime        ftLastModified;
    //[MarshalAs(UnmanagedType.Struct, SizeConst = 4)]
    public SortOrderSpec rgSortSpecs_0;
    public SortOrderSpec rgSortSpecs_1;
    public SortOrderSpec rgSortSpecs_2;
    public SortOrderSpec rgSortSpecs_3;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct CeRecordInfo {
    public ulong OidParent;
  }

  [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
  public struct CeOidInfo {
    [FieldOffset(0)]
    public int wObjType;

    [FieldOffset(4)]
    public int wPad;

    [FieldOffset(8)]
    public CeFileInfo   infFile;
    [FieldOffset(8)]
    public CeDirInfo    infDirectory;
    [FieldOffset(8)]
    public CeDBaseInfo  infDatabase;
    /*[FieldOffset(8)]
    public CeRecordInfo infRecord;*/
  }


  public enum ObjType : int {
    INVALID   = 0,
    FILE      = 1,
    DIRECTORY = 2,
    DATABASE  = 3,
    RECORD    = 4,
    DELETED   = 8,
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
