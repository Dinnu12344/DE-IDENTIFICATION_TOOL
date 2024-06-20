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

                // Retrieve the relative path from the configuration file

                string relativePath = ConfigurationManager.AppSettings["PythonScriptFilePath"];

                if (string.IsNullOrEmpty(relativePath))

                {

                    throw new ConfigurationErrorsException("PythonScriptFilePath is not configured.");

                }

                // Get the base directory of the current application

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Combine the base directory with the relative path to get the full path

                string fullPath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));

                // Check if the directory exists

                if (!Directory.Exists(fullPath))

                {

                    throw new DirectoryNotFoundException($"Python scripts directory not found: {fullPath}");

                }

                return fullPath;

            }

            catch (Exception ex)

            {

                // Log the exception details to a file for debugging

                string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error_log.txt");

                File.WriteAllText(logFilePath, $"{DateTime.Now}: {ex.ToString()}");

                // Re-throw the exception to be handled by the calling method

                throw;

            }

        }

    }

}
