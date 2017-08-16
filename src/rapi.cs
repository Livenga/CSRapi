using System;
using System.Runtime.InteropServices;


namespace CSRAPI
{
  /// <summary>Remote API Class</summary>
#region public class RAPI
  public class RAPI
  {
    //
    // �f�o�C�X�ڑ�����
    //
    [DllImport("kernel32.dll")]
      public static extern uint WaitForSingleObjectEx(
          int hHandle, uint dwMilliseconds);


    //
    // �f�o�C�X�̏������E�㏈��
    //
    [DllImport("rapi.dll",SetLastError=true)]
      public static extern void CeRapiInitEx(ref RAPIINIT pRapiInit);
    [DllImport("rapi.dll",SetLastError=true)]
      public static extern int CeRapiUninit();

    // �f�o�C�X�� OS ���擾
    [DllImport("rapi.dll",SetLastError=true)]
      public static extern int CeGetVersionEx(ref CEOSVERSIONINFO ceosver);

    //
    // �G���[�֌W
    //
    [DllImport("rapi.dll")]
      public static extern int CeRapiGetError();
    [DllImport("rapi.dll")]
      public static extern int CeGetLastError();


    //
    // �t�@�C������
    //
    // Open / Close
    [DllImport("rapi.dll",CharSet=CharSet.Unicode,SetLastError=true)]
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
    [DllImport("rapi.dll",CharSet=CharSet.Unicode,SetLastError=true)]
      public static extern bool CeReadFile(
          int     hFile,
          byte[]  lpBuffer,
          int     nNumberOfBytesToRead,
          out int lpNumberOfBytesRead,
          int     lpOverlapped);
    [DllImport("rapi.dll",CharSet=CharSet.Unicode,SetLastError=true)]
      public static extern int CeWriteFile(
          int     hFile,
          byte[]  lpBuffer,
          int     nNumberOfBytesToWrite,
          out int nNumberOfBytesWritten,
          int     lpOverlapped);

    // �t�@�C���T�C�Y�̎擾
    [DllImport("rapi.dll",SetLastError=true)]
      public static extern uint CeGetFileSize(IntPtr hFile, ref uint lpFileSizeHigh);
  }
#endregion
  
  /// <summary>Windows CE �ڑ����̗�O</summary>
#region public class CEConnectionException
  public class CEConnectionException : Exception
  {
    public CEConnectionException() : base() {}
    public CEConnectionException(string message) : base (message) {}
    public CEConnectionException(string message, Exception inner) : base(message, inner) {}
  }
#endregion
}
