using System;
using System.Runtime.InteropServices;

using CSRAPI;

namespace Live
{
  public class RAPITest
  {
    [STAThread]
      public static void Main(string[] args)
      {
        RAPIClient rapiClient;

        rapiClient = new RAPIClient(2000);
        try {
          rapiClient.Connect();
          rapiClient.Create(@"main.cs", @"\main.cs");
          //rapiClient.Copy(@"", "");
        }
        catch(Exception e) {
          Console.WriteLine(e.ToString());
          rapiClient.Disconnect();
        }
      }
  }
}
