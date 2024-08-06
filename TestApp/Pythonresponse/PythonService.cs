using DE_IDENTIFICATION_TOOL.Models;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

public class PythonService
{
    private readonly string pythonExePath;

    public PythonService()
    {
        pythonExePath = GetPythonExePath();
        if (string.IsNullOrEmpty(pythonExePath))
        {
            throw new Exception("Python executable not found in PATH.");
        }
    }


    //private string getpythonexepath()
    //{
    //    string pythonexename = "python.exe";
    //    string python = path.combine(appdomain.currentdomain.basedirectory+"scripts\\python", pythonexename);
    //    return python;
    //    //string[] paths = environment.getenvironmentvariable("path").split(';');
    //    //foreach (string path in paths)
    //    //{
    //    //    string fullpath = path.combine(path, pythonexename);
    //    //    if (file.exists(fullpath))
    //    //    {
    //    //        return fullpath;
    //    //    }
    //    //}
    //    //return null;
    //}

    //private string GetPythonExePath()
    //{
    //    // assuming python is installed in the specified directory
    //    string pythonexepath = @"c:\python312\python.exe";
    //    if (File.Exists(pythonexepath))
    //    {
    //        return pythonexepath;
    //    }
    //    else
    //    {
    //        throw new Exception("python executable not found at " + pythonexepath);
    //    }
    //}

    public string GetPythonExePath()
    {
        // Search in PATH environment variable
        string pathEnv = Environment.GetEnvironmentVariable("PATH");
        if (!string.IsNullOrEmpty(pathEnv))
        {
            string[] paths = pathEnv.Split(';');
            foreach (string path in paths)
            {
                string pythonPath = Path.Combine(path, "python.exe");
                if (File.Exists(pythonPath))
                {
                    return pythonPath;
                }
            }
        }

        // Search in registry
        string pythonPathFromRegistry = GetPythonPathFromRegistry();
        if (!string.IsNullOrEmpty(pythonPathFromRegistry) && File.Exists(pythonPathFromRegistry))
        {
            return pythonPathFromRegistry;
        }

        throw new Exception("Python executable not found in PATH or registry.");
    }

    private string GetPythonPathFromRegistry()
    {
        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Python\PythonCore"))
        {
            if (key != null)
            {
                foreach (var subkey in key.GetSubKeyNames())
                {
                    using (RegistryKey subKey = key.OpenSubKey(subkey))
                    {
                        if (subKey != null)
                        {
                            using (RegistryKey installPathKey = subKey.OpenSubKey("InstallPath"))
                            {
                                if (installPathKey != null)
                                {
                                    string pythonExePath = Path.Combine(installPathKey.GetValue("").ToString(), "python.exe");
                                    if (File.Exists(pythonExePath))
                                    {
                                        return pythonExePath;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return null;
    }


    public string checkDeidentifiedTable(string table, string project, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{project}\" \"{table}\"";
        return ExecutePythonScript(command);
    }
    public string SendDataToPython(string table, string project, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{project}\" \"{table}\"";
        return ExecutePythonScript(command);
    }
    public string JsonImport( string path ,string  projectName, string n,string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }
        var command = $"\"{pythonScriptPath}\" \"{path}\" \"{projectName}\" \"{n}\"";
        return ExecutePythonScript(command);
    }


    public string RenameTableDataToPython(string projectName, string oldTableName , string newTableName, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{projectName}\" \"{oldTableName}\" \"{newTableName}\"";
        return ExecutePythonScript(command);
    }


    public string RenameDataToPython(string project, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{project}\"";
        return ExecutePythonScript(command);
    }
    //projectName,server, DatabaseName, userId, password, TableName, jsonData, Enterno, savePythonScriptPath
    public string SendSqlDataToPython(string projectName, string server, string DatabaseName, string userId, string password, string TableName, string jsonData, string Enterno, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{projectName}\" \"{server}\" \"{DatabaseName}\" \"{userId}\"  \"{password}\" \"{TableName}\" \"{jsonData}\" \"{Enterno}\"";
        return ExecutePythonScript(command);
    }

    public string SendSqlImportDataToPython(string server, string DatabaseName, string password, string userId, string projectName, string EnteredText, string tableName, string schemaName, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{server}\" \"{DatabaseName}\" \"{password}\" \"{userId}\"  \"{projectName}\" \"{EnteredText}\" \"{tableName}\" \"{schemaName}\"";
        return ExecutePythonScript(command);
    }
    public string SendDataToPython(string filePath, string project, string table, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{filePath}\" \"{project}\" \"{table}\"";
        return ExecutePythonScript(command);
    }

    public string DeleteData(string project, string table, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{project}\" \"{table}\"";
        return ExecutePythonScript(command);
    }

    public string DeleteProjectData(string project, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{project}\"";
        return ExecutePythonScript(command);
    }

    public string SendDataToPython(string SelectedCsvFilePath, string projectName, string TableName, string SelectedDelimiter, string SelectedQuote, string EnteredText, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{SelectedCsvFilePath}\" \"{projectName}\" \"{EnteredText}\" \"{TableName}\" \"{SelectedDelimiter}\" \"{SelectedQuote}\"";
        return ExecutePythonScript(command);
    }

    public string SendSqlExportDataToPython(string server, string database, string password, string UserId, string schemaName, string projectName, string mainTableName, string tableName, string savePythonScriptPath)
    {
        if (!File.Exists(savePythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{savePythonScriptPath}\" \"{server}\" \"{database}\" \"{password}\" \"{UserId}\" \"{schemaName}\" \"{projectName}\" \"{mainTableName}\" \"{tableName}\"";
        return ExecutePythonScript(command);
    }



    public string SendSqlExportAllDataToPython(string server, string database, string password, string UserId, string projectName, string savePythonScriptPath)
    {
        if (!File.Exists(savePythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        // Ensure a space between UserId and projectName
        var command = $"\"{savePythonScriptPath}\" \"{server}\" \"{database}\" \"{password}\" \"{UserId}\" \"{projectName}\"";
        return ExecutePythonScript(command);
    }



    private string ExecutePythonScript(string command)
    {
        using (Process process = new Process())
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = GetPythonExePath();
            startInfo.Arguments = command;
            startInfo.StandardOutputEncoding = Encoding.UTF8;

            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }
    }
}
