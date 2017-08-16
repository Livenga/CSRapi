using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CSRAPI
{
  public class RAPIClient {
    /// <summary>接続状態の確認</summary>
    public bool IsConnected { get { return this.isConnected; } }


    private uint timeout     = 2000;
    private bool isConnected = false;


    /// <summary>コンストラクタ</summary>
    public RAPIClient() : this(2000) {}
    public RAPIClient(uint timeout) { this.timeout = timeout; }

    /// <summary>接続</summary>
#region public void Connect()
    public void Connect()
    {
      uint     wait_result = 0;
      RAPIINIT rinit;

      rinit        = new RAPIINIT();
      rinit.cbSize = Marshal.SizeOf(rinit);

      RAPI.CeRapiInitEx(ref rinit);
      wait_result  = RAPI.WaitForSingleObjectEx((int)rinit.heRapiInit, this.timeout);
      if(rinit.hrRapiInit == 0) { // 接続成功
        this.isConnected = true;
      }
      else { // 接続失敗
        this.isConnected = false;
        throw new CEConnectionException("Windows CE への接続に失敗", new Exception());
      }
    }
#endregion

    /// <summary>切断</summary>
#region public  void Disconnect()
    public void Disconnect()
    {
      if(!this.isConnected) return;
      RAPI.CeRapiUninit();
    }
#endregion


    /// <summary>Windows CE にファイルを転送(上書き)</summary>
    /// <param name="sourceFileName">ローカルの対象ファイルパス</param>
    /// <param name="destFileName">Windows CE に保存するパス</param>
#region public void Create(string, string)
    public void Create(string sourceFileName, string destFileName)
    {
      int handle;
      byte[] ctx = null;

      if(!this.isConnected) {
        Exception e = new Exception();
        throw new CEConnectionException("Windows CE が接続されていません.", e);
      }

      handle = RAPI.CeCreateFile(destFileName,
          (uint)DesiredAccess.GENERIC_WRITE,
          (int)ShareMode.FILE_SHARE_WRITE,
          0,
          (int)CreationDisposition.CREATE_ALWAYS,
          (int)FlagsAndAttributes.FILE_ATTRIBUTE_NORMAL,
          0);

      if(handle == -1) {
        // TODO: エラーコード別のメッセージの取得
        int err = RAPI.CeGetLastError();
#if _DEBUG_
        Console.WriteLine("### [D] Error Code: {0} ###", err);
#endif
        throw new Exception("CeCreateFile: ファイル作成時にエラーが発生しました.");
      }


      try {
        using(FileStream strm = new FileStream(sourceFileName, FileMode.Open)) {
          int n;

          // [local] ファイルの読み込み
          ctx = new byte[strm.Length];
          strm.Read(ctx, 0, (int)strm.Length);

          // [Windows CE] ファイルの書き込み
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


    /// <summary>Windows CE 内のファイルを取得</summary>
    /// <param name="sourceFileName">Windows CE の対象ファイルパス</param>
    /// <param name="destFileName">ローカルに保存するパス</param>
#region public void Copy(string, string)
    public void Copy(string sourceFileName, string destFileName)
    {
      int    handle;
      uint   file_size, ref_value = 0;
      
      int    n;
      byte[] ctx = null;


      if(!this.isConnected) {
        Exception e = new Exception();
        throw new CEConnectionException("Windows CE が接続されていません.", e);
      }

      using(FileStream strm = new FileStream(destFileName, FileMode.Create)) {
        // Windows CE からファイルの読み込み
        handle    = RAPI.CeCreateFile(
            destFileName,
            (uint)DesiredAccess.GENERIC_READ,
            (int)ShareMode.FILE_SHARE_READ,
            0,
            (int)CreationDisposition.OPEN_ALWAYS,
            (int)FlagsAndAttributes.FILE_ATTRIBUTE_NORMAL,
            0);

        // ファイルが存在しない場合, 例外を投げる.
        if(handle < 0)
          throw new FileNotFoundException(
              "Windows CEデバイス内に " + destFileName + " は存在しません.");

        file_size = RAPI.CeGetFileSize((IntPtr)handle, ref ref_value);
        ctx       = new byte[file_size];
        RAPI.CeReadFile(handle, ctx, (int)file_size, out n, 0);
        RAPI.CeCloseHandle(handle);

#if _DEBUG_
        Console.WriteLine("### [D] {0} size {1} ###", destFileName, file_size);
#endif

        // ファイルの書き込み
        strm.Write(ctx, 0, n);
      }
    }
#endregion
  }
}
