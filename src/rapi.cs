using System;
using System.IO;
using System.Runtime.InteropServices;

using CEOID = System.UInt64;


namespace CSRAPI {
  /// <summary>Remote API Class</summary>
#region public class RAPI
  public class RAPI {
    //
    // デバイス接続操作
    //
    [DllImport("kernel32.dll")]
      public static extern uint WaitForSingleObjectEx(
          int hHandle, uint dwMilliseconds);


    //
    // デバイスの初期化・後処理
    //
    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      public static extern void CeRapiInitEx(ref RAPIINIT pRapiInit);
    [DllImport("rapi.dll",SetLastError=true)]
      public static extern int CeRapiUninit();

    // デバイスの OS 情報取得
    [DllImport("rapi.dll",SetLastError=true)]
      public static extern int CeGetVersionEx(ref CEOSVERSIONINFO ceosver);

    //
    [DllImport("rapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool CeGetSystemPowerStatusEx(
        ref SystemPowerStatusEx pstatus,
            bool                fUpdate);

    [DllImport("rapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern UInt32 CeGetTempPath(
            UInt32 nBufferLength,
        ref string lpBuffer);

    [DllImport("rapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern void CeGlobalMemoryStatus(ref MemoryStatus lpmst);

    //
    // エラー関係
    //

    [DllImport("rapi.dll", CharSet = CharSet.Unicode)]
      public static extern int CeRapiGetError();
    [DllImport("rapi.dll", CharSet = CharSet.Unicode)]
      public static extern int CeGetLastError();

    //
    //
    //

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeGetStoreInformation(ref StoreInformation lpsi);

    //
    // ファイル操作
    //

    // Open / Close
    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int CeCreateFile(
        string lpFileName,
        uint   dwDesiredAccess,
        int    dwShareMode,
        int    lpSecurityAttributes,
        int    dwCreationDisposition,
        int    dwFlagsAndAttributes,
        int    hTemplateFile);

    [DllImport("rapi.dll",SetLastError=true)]
    public static extern bool CeCloseHandle(int hObject);

    // Read / Write
    [DllImport("rapi.dll",CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeReadFile(
            int    hFile,
            byte[] lpBuffer,
            int    nNumberOfBytesToRead,
        out int    lpNumberOfBytesRead,
            int    lpOverlapped);

    [DllImport("rapi.dll",CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int CeWriteFile(
            int    hFile,
            byte[] lpBuffer,
            int    nNumberOfBytesToWrite,
        out int    nNumberOfBytesWritten,
            int    lpOverlapped);

    // ファイルの削除
    [DllImport("rapi.dll",CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeDeleteFile(string lpFileName);

    // ファイルサイズの取得
    [DllImport("rapi.dll", SetLastError = true)]
    public static extern uint CeGetFileSize(
            IntPtr hFile,
        ref uint   lpFileSizeHigh);

    // ファイル特性の設定
    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      public static extern bool CeSetFileAttributes(
          string         lpFileName,
          FileAttributes dwFileAttributes);

    // ファイル特性の取得
    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern ulong CeGetFileAttributes(string lpFileName);

    // ファイルの移動
    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int CeMoveFile(
        string sExistingFileName,
        string sNewFileName);

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr CeFindFirstFile(
            string     lpFileName,
        ref CeFindData lpFindFileData);
    
    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeFindNextFile(
            //int        hFindFile,
            IntPtr     hFindFile,
        ref CeFindData lpFindFileData);

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeFindClose(IntPtr handle);


    //
    // ディレクトリ
    //

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeCreateDirectory(
        string lpPathName,
        IntPtr lpSecurityAttributes);

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeRemoveDirectory(string lpPathName);


    //
    // データベース
    //

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr CeFindFirstDatabase(UInt64 dwDbaseType);

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern CEOID CeFindNextDatabase(IntPtr hEnum);

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeOidGetInfo(CEOID oid, ref CeOidInfo pOidInfo);


    //
    // プロセス
    //

    /// <summary></summary>
    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeCreateProcess(
            string             lpApplicationName,
            string             lpCommandLine,
            IntPtr             lpProcessAttributes, // Not supported, set to Null
            IntPtr             lpThreadAttributes,  // Not supported, set to null
            bool               bInheritHandles,     // Not supported, set to false
            ulong              dwCreationFlags, // 0
            IntPtr             lpEnvironment,       // Not supported, set to null
            IntPtr             lpCurrentDirectory,  // Not supported, set to null
            IntPtr             lpStartupInfo,       // Not supported, set to null
        ref ProcessInformation lpProcessInformation);
  }
#endregion
  
  /// <summary>Windows CE 接続時の例外</summary>
#region public class CEConnectionException
  public class CEConnectionException : Exception {
    public CEConnectionException() : base() {}
    public CEConnectionException(string message) : base (message) {}
    public CEConnectionException(string message, Exception inner) : base(message, inner) {}
  }
#endregion
}
