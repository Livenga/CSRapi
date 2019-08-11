using System;
using System.IO;
using System.Runtime.InteropServices;

using CEOID = System.UInt64;


namespace CSRAPI {
  /// <summary>Remote API Class</summary>
#region public class RAPI
  public class RAPI {
    //
    // �f�o�C�X�ڑ�����
    //
    [DllImport("kernel32.dll")]
      public static extern uint WaitForSingleObjectEx(
          int hHandle, uint dwMilliseconds);


    //
    // �f�o�C�X�̏������E�㏈��
    //
    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      public static extern void CeRapiInitEx(ref RAPIINIT pRapiInit);
    [DllImport("rapi.dll",SetLastError=true)]
      public static extern int CeRapiUninit();

    // �f�o�C�X�� OS ���擾
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
    // �G���[�֌W
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
    // �t�@�C������
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

    // �t�@�C���̍폜
    [DllImport("rapi.dll",CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeDeleteFile(string lpFileName);

    // �t�@�C���T�C�Y�̎擾
    [DllImport("rapi.dll", SetLastError = true)]
    public static extern uint CeGetFileSize(
            IntPtr hFile,
        ref uint   lpFileSizeHigh);

    // �t�@�C�������̐ݒ�
    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
      public static extern bool CeSetFileAttributes(
          string         lpFileName,
          FileAttributes dwFileAttributes);

    // �t�@�C�������̎擾
    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern ulong CeGetFileAttributes(string lpFileName);

    // �t�@�C���̈ړ�
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
    // �f�B���N�g��
    //

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeCreateDirectory(
        string lpPathName,
        IntPtr lpSecurityAttributes);

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeRemoveDirectory(string lpPathName);


    //
    // �f�[�^�x�[�X
    //

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr CeFindFirstDatabase(UInt64 dwDbaseType);

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern CEOID CeFindNextDatabase(IntPtr hEnum);

    [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CeOidGetInfo(CEOID oid, ref CeOidInfo pOidInfo);


    //
    // �v���Z�X
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
  
  /// <summary>Windows CE �ڑ����̗�O</summary>
#region public class CEConnectionException
  public class CEConnectionException : Exception {
    public CEConnectionException() : base() {}
    public CEConnectionException(string message) : base (message) {}
    public CEConnectionException(string message, Exception inner) : base(message, inner) {}
  }
#endregion
}
