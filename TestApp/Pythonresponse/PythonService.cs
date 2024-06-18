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

    private string GetPythonExePath()
    {
        string pythonExeName = "python.exe";
        string[] paths = Environment.GetEnvironmentVariable("PATH").Split(';');
        foreach (string path in paths)
        {
            string fullPath = Path.Combine(path, pythonExeName);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }
        return null;
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
    public string SendSqlImportDataToPython(string server, string DatabaseName, string password, string userId, string projectName, string EnteredText, string tableName,string schemaName, string pythonScriptPath)
    {
        if (!File.Exists(pythonScriptPath))
        {
            return "Error: Python script file not found.";
        }

        var command = $"\"{pythonScriptPath}\" \"{server}\" \"{DatabaseName}\" \"{password}\" \"{userId}\"  \"{projectName}\" \"{EnteredText}\" \"{tableName}\" \"{schemaName}\"";
        return ExecutePythonScript(command);
    }

    public string SendDataToPython(string filePath, string project,string table, string pythonScriptPath)        
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

        var command = $"\"{savePythonScriptPath}\" \"{server}\" \"{projectName}\" \"{database}\" \"{password}\" \"{UserId}\" \"{schemaName}\" \"{projectName}\" \"{mainTableName}\" \"{tableName}\"";
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
