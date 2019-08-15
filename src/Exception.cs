using System;
using System.ComponentModel;


namespace CSRAPI {
  public class CeRapiException : Win32Exception {
    public int Win32ErrorCode {
      get {
        return this.win32ErrorCode;
      }
    }

    private int win32ErrorCode;

    public CeRapiException(string message) : base(message) {}
    public CeRapiException(int win32ErrorCode) : base(win32ErrorCode) {
      this.win32ErrorCode = win32ErrorCode;
#if DEBUG
      Console.Error.WriteLine(this.Win32ErrorCode);
      Console.Error.WriteLine(this.Message);
#endif
    }
  }
}
