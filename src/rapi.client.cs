using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CSRAPI
{
  public class RAPIClient {
    /// <summary>�ڑ���Ԃ̊m�F</summary>
    public bool IsConnected { get { return this.isConnected; } }


    private uint timeout     = 2000;
    private bool isConnected = false;


    /// <summary>�R���X�g���N�^</summary>
    public RAPIClient() : this(2000) {}
    public RAPIClient(uint timeout) { this.timeout = timeout; }

    /// <summary>�ڑ�</summary>
#region public void Connect()
    public void Connect()
    {
      uint     wait_result = 0;
      RAPIINIT rinit;

      rinit        = new RAPIINIT();
      rinit.cbSize = Marshal.SizeOf(rinit);

      RAPI.CeRapiInitEx(ref rinit);
      wait_result  = RAPI.WaitForSingleObjectEx((int)rinit.heRapiInit, this.timeout);
      if(rinit.hrRapiInit == 0) { // �ڑ�����
        this.isConnected = true;
      }
      else { // �ڑ����s
        this.isConnected = false;
        throw new CEConnectionException("Windows CE �ւ̐ڑ��Ɏ��s", new Exception());
      }
    }
#endregion

    /// <summary>�ؒf</summary>
#region public  void Disconnect()
    public void Disconnect()
    {
      if(!this.isConnected) return;
      RAPI.CeRapiUninit();
    }
#endregion


    /// <summary>Windows CE �Ƀt�@�C����]��(�㏑��)</summary>
    /// <param name="sourceFileName">���[�J���̑Ώۃt�@�C���p�X</param>
    /// <param name="destFileName">Windows CE �ɕۑ�����p�X</param>
#region public void Create(string, string)
    public void Create(string sourceFileName, string destFileName)
    {
      int handle;
      byte[] ctx = null;

      if(!this.isConnected) {
        Exception e = new Exception();
        throw new CEConnectionException("Windows CE ���ڑ�����Ă��܂���.", e);
      }

      handle = RAPI.CeCreateFile(destFileName,
          (uint)DesiredAccess.GENERIC_WRITE,
          (int)ShareMode.FILE_SHARE_WRITE,
          0,
          (int)CreationDisposition.CREATE_ALWAYS,
          (int)FlagsAndAttributes.FILE_ATTRIBUTE_NORMAL,
          0);

      if(handle == -1) {
        // TODO: �G���[�R�[�h�ʂ̃��b�Z�[�W�̎擾
        int err = RAPI.CeGetLastError();
#if _DEBUG_
        Console.WriteLine("### [D] Error Code: {0} ###", err);
#endif
        throw new Exception("CeCreateFile: �t�@�C���쐬���ɃG���[���������܂���.");
      }


      try {
        using(FileStream strm = new FileStream(sourceFileName, FileMode.Open)) {
          int n;

          // [local] �t�@�C���̓ǂݍ���
          ctx = new byte[strm.Length];
          strm.Read(ctx, 0, (int)strm.Length);

          // [Windows CE] �t�@�C���̏�������
          RAPI.CeWriteFile(handle, ctx, (int)strm.Length, out n, 0);
          RAPI.CeCloseHandle(handle);
        }
      }
      catch(Exception e) {
        RAPI.CeCloseHandle(handle);
        throw e;
      }
    }
#endregion


    /// <summary>Windows CE ���̃t�@�C�����擾</summary>
    /// <param name="sourceFileName">Windows CE �̑Ώۃt�@�C���p�X</param>
    /// <param name="destFileName">���[�J���ɕۑ�����p�X</param>
#region public void Copy(string, string)
    public void Copy(string sourceFileName, string destFileName)
    {
      int    handle;
      uint   file_size, ref_value = 0;
      
      int    n;
      byte[] ctx = null;


      if(!this.isConnected) {
        Exception e = new Exception();
        throw new CEConnectionException("Windows CE ���ڑ�����Ă��܂���.", e);
      }

      using(FileStream strm = new FileStream(destFileName, FileMode.Create)) {
        // Windows CE ����t�@�C���̓ǂݍ���
        handle    = RAPI.CeCreateFile(
            destFileName,
            (uint)DesiredAccess.GENERIC_READ,
            (int)ShareMode.FILE_SHARE_READ,
            0,
            (int)CreationDisposition.OPEN_ALWAYS,
            (int)FlagsAndAttributes.FILE_ATTRIBUTE_NORMAL,
            0);

        // �t�@�C�������݂��Ȃ��ꍇ, ��O�𓊂���.
        if(handle < 0)
          throw new FileNotFoundException(
              "Windows CE�f�o�C�X���� " + destFileName + " �͑��݂��܂���.");

        file_size = RAPI.CeGetFileSize((IntPtr)handle, ref ref_value);
        ctx       = new byte[file_size];
        RAPI.CeReadFile(handle, ctx, (int)file_size, out n, 0);
        RAPI.CeCloseHandle(handle);

#if _DEBUG_
        Console.WriteLine("### [D] {0} size {1} ###", destFileName, file_size);
#endif

        // �t�@�C���̏�������
        strm.Write(ctx, 0, n);
      }
    }
#endregion
  }
}
