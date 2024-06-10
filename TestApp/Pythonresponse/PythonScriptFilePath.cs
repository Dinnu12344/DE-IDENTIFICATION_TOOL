using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_IDENTIFICATION_TOOL.Pythonresponse
{
    public class PythonScriptFilePath
    {
        public static string FindProjectRootDirectory()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string previousDirectory = "";

            while (!Directory.GetFiles(currentDirectory, "*.sln").Any())
            {
                previousDirectory = currentDirectory;
                currentDirectory = Directory.GetParent(currentDirectory).FullName;
            }

            return previousDirectory;
        }
    }
}
