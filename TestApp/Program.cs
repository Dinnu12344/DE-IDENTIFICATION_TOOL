using DE_IDENTIFICATION_TOOL.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE_IDENTIFICATION_TOOL
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string logFilePath = null;
            string scriptPath = null;
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (string.IsNullOrEmpty(baseDirectory))
                {
                    throw new Exception("Base directory is null or empty");
                }

                logFilePath = Path.Combine(baseDirectory, "log.txt");
                scriptPath = Path.Combine(baseDirectory, "Scripts", "install_python.bat");

                File.AppendAllText(logFilePath, $"Base directory: {baseDirectory}\n");
                File.AppendAllText(logFilePath, $"Log file path: {logFilePath}\n");
                File.AppendAllText(logFilePath, $"Script path: {scriptPath}\n");

                if (!File.Exists(scriptPath))
                {
                    throw new FileNotFoundException($"Batch script not found at {scriptPath}");
                }

                File.AppendAllText(logFilePath, $"Starting application at {DateTime.Now}\n");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!IsPythonInstalled())
                {
                    LoadingForm loadingForm = new LoadingForm();
                    loadingForm.Show();

                    Task.Run(() =>
                    {
                        try
                        {
                            ExecuteBatchScript(scriptPath, logFilePath);
                            loadingForm.Invoke(new Action(() =>
                            {
                                loadingForm.Close();
                                DialogResult result = MessageBox.Show("Python installed successfully. Launching application...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (result == DialogResult.OK)
                                {
                                    Application.Run(new HomeForm());
                                }
                            }));
                        }
                        catch (Exception ex)
                        {
                            loadingForm.Invoke(new Action(() =>
                            {
                                loadingForm.Close();
                                DialogResult result = MessageBox.Show($"Error installing Python: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (result == DialogResult.OK)
                                {
                                    Application.Exit();
                                }
                            }));
                        }
                    });

                    Application.Run(loadingForm);
                }
                else
                {
                    File.AppendAllText(logFilePath, $"Python is already installed. Launching application...\n");
                    ExecuteBatchScript(scriptPath, logFilePath);
                    Application.Run(new HomeForm());
                }

                File.AppendAllText(logFilePath, $"Application exited normally at {DateTime.Now}\n");
            }
            catch (Exception ex)
            {
                if (logFilePath != null)
                {
                    File.AppendAllText(logFilePath, $"Error: {ex.Message}\n");
                    if (ex.InnerException != null)
                    {
                        File.AppendAllText(logFilePath, $"Inner Exception: {ex.InnerException.Message}\n");
                    }
                }
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        private static bool IsPythonInstalled()
        {
            try
            {
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c \"python --version\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    process.StartInfo = startInfo;

                    process.Start();
                    process.WaitForExit();

                    return process.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while checking Python installation: {ex.Message}");
                return false;
            }
        }

        public static void ExecuteBatchScript(string scriptPath, string logFilePath)
        {
            try
            {
                Console.WriteLine($"Executing batch script: {scriptPath}");
                File.AppendAllText(logFilePath, $"Executing batch script: {scriptPath}\n");

                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c \"{scriptPath}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    process.StartInfo = startInfo;

                    process.OutputDataReceived += (sender, arg) =>
                    {
                        if (arg.Data != null)
                        {
                            Console.WriteLine($"Output: {arg.Data}");
                            File.AppendAllText(logFilePath, $"Output: {arg.Data}\n");
                        }
                    };
                    process.ErrorDataReceived += (sender, arg) =>
                    {
                        if (arg.Data != null)
                        {
                            Console.WriteLine($"Error: {arg.Data}");
                            File.AppendAllText(logFilePath, $"Error: {arg.Data}\n");
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        string error = $"Batch script execution failed with exit code {process.ExitCode}.";
                        File.AppendAllText(logFilePath, $"Error: {error}\n");
                        throw new Exception(error);
                    }
                }

                Console.WriteLine("Batch script executed successfully.");
                File.AppendAllText(logFilePath, "Batch script executed successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                File.AppendAllText(logFilePath, $"Exception occurred: {ex.Message}\n");
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }
    }
}
