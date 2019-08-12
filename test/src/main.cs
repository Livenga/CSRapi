using System;
using System.IO;
using System.Runtime.InteropServices;

using CSRAPI;
using CEOID = System.UInt64;


namespace Live {
  public class RAPITest {
    [STAThread]
    public static void Main(string[] args) {
      RAPIClient cli;

      try {
        cli = new RAPIClient(2000);
        cli.Connect();


        cli.Disconnect();
      } catch(Exception except) {
        Console.Error.WriteLine(except.GetType().ToString());
        Console.Error.WriteLine(except.Message);
        Console.Error.WriteLine(except.StackTrace);
      }
    }
  }
}
