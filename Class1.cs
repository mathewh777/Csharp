using System;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;
using System.Diagnostics;
//using System.Management.Automation; //.Runspaces.RunspaceFactory;

//$path = "C:\Users\Laptop\Desktop\c source\basdll\Class1.dll"
//[System.Reflection.Assembly]::LoadFrom($path)
//C:\Windows\System32\cmd.exe
//Complie file //C:\WINDOWS\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:library Class1.cs
public class win
{
    private static string runspace(){
        CreateRunspace RS = new RunspaceFactory.CreateRunspace();
        RS.Open();
        var Pipe = RS.CreatePipeline();
        Pipe.Commands.AddScript("whoami");
        return Pipe.Invoke();
    }
    private static string shell(string com)
    {
        Process Exec = new Process();
        Exec.StartInfo.FileName = "powershell.exe";
        Exec.StartInfo.Arguments = "/c " + com;
        Exec.StartInfo.CreateNoWindow = true;
        Exec.StartInfo.RedirectStandardInput = true;
        Exec.StartInfo.RedirectStandardOutput = true;
        Exec.StartInfo.UseShellExecute = false;
        //Exec.WorkingDirectory = @"C:\Windows";
        Exec.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        Exec.Start();
        string Output = Exec.StandardOutput.ReadToEnd();
        Exec.Dispose();
        Exec.Close();
        return Output;
    }
    private static string client(string url)
    {
        //switch (mode) { mode == "u": something; break;}
        WebClient client = new WebClient();
        byte[] data = client.DownloadData(url);
        // [System.IO.File]::ReadAllBytes($a)
        string StrData = Encoding.ASCII.GetString(data);
        return StrData;
    }
    public static string main(string com) //entry point
    {
        //string data = client("https://example.com");
        //string data = "exec";
        //string ExecOut = shell(com);
        string ExecOut = runspace();
        return ExecOut;
    }
}

