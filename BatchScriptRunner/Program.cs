using System;
using System.Diagnostics;
using System.IO;

namespace BatchScriptRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string batchFilePath = args.Length > 0 ? args[0] : "Scripts\\install_python.bat";
                ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + batchFilePath);
                processInfo.CreateNoWindow = false;
                processInfo.UseShellExecute = false;
                processInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                Process process = Process.Start(processInfo);
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Batch script execution failed.");
                }
                else
                {
                    Console.WriteLine("Batch script executed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
