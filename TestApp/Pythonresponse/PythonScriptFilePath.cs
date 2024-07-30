using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace DE_IDENTIFICATION_TOOL.Pythonresponse

{
    public class PythonScriptFilePath
    {
        public static string FindProjectRootDirectory()
        {
            try
            {
                string relativePath = ConfigurationManager.AppSettings["PythonScriptFilePath"];
                if (string.IsNullOrEmpty(relativePath))
                {
                    throw new ConfigurationErrorsException("PythonScriptFilePath is not configured.");
                }

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));
                if (!Directory.Exists(fullPath))
                {
                    throw new DirectoryNotFoundException($"Python scripts directory not found: {fullPath}");
                }
                return fullPath;
            }
            catch (Exception ex)
            {
                string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error_log.txt");
                File.WriteAllText(logFilePath, $"{DateTime.Now}: {ex.ToString()}"); 
                throw;
            }
        }
    }
}
