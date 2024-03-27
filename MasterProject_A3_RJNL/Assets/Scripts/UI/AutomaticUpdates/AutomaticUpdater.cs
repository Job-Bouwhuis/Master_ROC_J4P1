// Creator: Job
using ShadowUprising.UI.Loading;
using System;
using TMPro;
using UnityEngine;
using WinterRose.WIP.Redis;
using System.IO;
using WinterRose.FileManagement;
using WinterRose;
using System.Threading;
using System.Threading.Tasks;
using ShadowUprising.Utils;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ShadowUprising.AutoUpdates
{
    /// <summary>
    /// Component responsible for downloading the latest update from the server and starting the installer application.
    /// </summary>
    public class AutomaticUpdater : MonoBehaviour
    {
        /// <summary>
        /// Text component to display the current version and the version being updated to.
        /// </summary>
        public TMP_Text versionText;
        /// <summary>
        /// Text component to display the log found from <see cref="Log"/>
        /// </summary>
        public TMP_Text logText;
        /// <summary>
        /// Text component to display the progress of the download.
        /// </summary>
        public TMP_Text progressText;

        /// <summary>
        /// The progress bar to display the download progress.
        /// </summary>
        public ProgressBar progressBar;

        private RedisConnection redis;
        private bool consoleWasOpen = false;
        private bool isDownloadComplete = false;
        private bool hasFaulted = false;
        private Thread downloadTask;
        private string downloadedPackage;
        private bool lastVisualupdate = false;

        // Start is called before the first frame update
        private void Start()
        {
#if UNITY_EDITOR
            Log.PushWarning("Updates can not be happening in unity editor. that can corrupt the unity editor install.");
            LoadingScreen.Instance.LoadWithoutShow("MainMenu");
            return;
#endif

            // Create a new Process object.

            bool found = IsDotNET8Installed();
            if (!found)
            {
                Windows.DialogResult result = Windows.MessageBox("Failed to find .NET 8.0 SDK on your machine.\nThis is required for automatic updates to be available.\n\n" +
                    "Would you like to install this now?", ".NET 8.0 not installed", Windows.MessageBoxButtons.YesNo, Windows.MessageBoxIcon.Error);

                if (result == Windows.DialogResult.Yes)
                {
                    Windows.MessageBox("Please enter 'y' whenever the console window that will pop up momentarily asks for it.\n" +
                        "it is required for the .NET 8.0 to be installed");

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c winget install Microsoft.DotNet.DesktopRuntime.8",
                        UseShellExecute = true,
                    };

                    Process cmd = Process.Start(startInfo);

                    cmd.WaitForExit();
                }
                else
                {
                    Windows.MessageBox("Automatic updates will not be available. loading Main Menu so the game can still be played.",
                        "Automatic updates not available", Windows.MessageBoxButtons.OK, Windows.MessageBoxIcon.Exclamation);

                    LoadingScreen.Instance.ShowAndLoad("MainMenu");
                    return;
                }
            }
            if (!IsDotNET8Installed())
            {
                Windows.DialogResult result = Windows.MessageBox("Failed to install .NET 8.0 automatically. Download manually?",
                    ".NET8 download failed",
                    Windows.MessageBoxButtons.YesNo, Windows.MessageBoxIcon.Error);

                if (result == Windows.DialogResult.Yes)
                {
                    // open browser to the download page depending on the architecture of the machine.
                    // always use windows platform as the game is only available on windows.

                    if(RuntimeInformation.OSArchitecture == Architecture.X64)
                    {
                        Process.Start("https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-8.0.100-windows-x64-installer");
                    }
                    else
                    {
                        Process.Start("https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-8.0.100-windows-x86-installer");
                    }

                    Windows.MessageBox("Please install .NET 8.0 manually and restart the game.", "Manual installation required", Windows.MessageBoxButtons.OK, Windows.MessageBoxIcon.Information);
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    Windows.MessageBox("Automatic updates will not be available. loading Main Menu so the game can still be played.",
                                               "Automatic updates not available", Windows.MessageBoxButtons.OK, Windows.MessageBoxIcon.Exclamation);

                    LoadingScreen.Instance.ShowAndLoad("MainMenu");
                    return;
                }
            }
            Log.Push(".NET 8.0 installed. Proceeding with update...");

            logText.text = "";
            Log.OnLogPushed += Log_OnLogPushed;
            consoleWasOpen = Log.ConsoleEnabled;
            Log.ConsoleEnabled = true;
            Log.Push("Connecting to Database...");
            redis = new RedisConnection();
            try
            {
                redis.MakeConnection("212.132.69.23", 6379);
                redis.Authenticate("q.$p.I>%");
            }
            catch
            {
                Log.PushError("Failed to connect to Database.");
                Log.PushWarning("Automatic updates will not be available. loading Main Menu so the game can still be played.");

                redis.Dispose();
                LoadingScreen.Instance.LoadWithoutShow("MainMenu");
                return;

            }
            Log.Push("Connected to Database.");

            if (!consoleWasOpen)
                Log.ConsoleEnabled = false;

            string serverVersion = redis.GetHashFieldValue("A3Games::GameData", "Version");
            string current = Resources.Load<TextAsset>("Version/GameVersion").text;

            versionText.text = $"Updating game from Update {current} to Update {serverVersion}";

            Log.Push("Downloading update...");
            downloadTask = new Thread(DownloadUpdate);
            downloadTask.Start();
        }
        private static bool IsDotNET8Installed()
        {
            DirectoryInfo dotNET = new("C:\\Program Files\\dotnet\\sdk");
            bool found = false;
            foreach (var dir in dotNET.GetDirectories())
            {
                if (dir.Name.Contains("8.0"))
                {
                    found = true;
                    break;
                }
            }

            return found;
        }
        private void Log_OnLogPushed(Log.LogEventArgs args)
        {
            if (args.message is "Done")
                return;
            logText.text += args.message + "\n";
        }
        private void Update()
        {
            try
            {
                if (downloadTask is null)
                    return;
                if (hasFaulted)
                {
                    Log.PushError("Failed to download update.");
                    Log.PushWarning("Automatic updates will not be available. loading Main Menu so the game can still be played.");

                    LoadingScreen.Instance.LoadWithoutShow("MainMenu");
                    return;
                }
                if (!isDownloadComplete)
                    return;

                if (isDownloadComplete)
                    Log.Push("Downloaded update.");
                else
                    return;

                if (!lastVisualupdate)
                {
                    downloadTask.Join();
                    Log.Push("Preparing to install...");
                    progressText.text = "Preparing to install...";
                    lastVisualupdate = true;
                    return;
                }

                var dir = new DirectoryInfo(Path.GetTempPath() + "A3Games");
                if (!dir.Exists)
                    dir.Create();
                FileInfo packageFile = new FileInfo(Path.GetTempPath() + "A3Games\\GameFiles.apkg");
                if (packageFile.Exists)
                    packageFile.Delete();

                FileManager.Write(packageFile.FullName, downloadedPackage);

                // TODO: do a quick install of the insta ller and start the installer to install the update. most insane sentence ive ever written.

                DirectoryInfo installerDir = new(Path.GetTempPath() + "A3Games" + "\\Installer");
                DirectoryInfo installerPath = new(Path.GetTempPath() + "A3Games\\Installer\\WinterRosePackageInstaller.exe");
                DirectoryInfo internalInstallerDir = new(Application.streamingAssetsPath.Replace('/', '\\') + @"\AppInstaller");

                if (!internalInstallerDir.Exists)
                {
                    Log.PushError("Internal installer not found.");
                    Log.PushWarning("Automatic updates will not be available. loading Main Menu so the game can still be played.");

                    LoadingScreen.Instance.LoadWithoutShow("MainMenu");
                    return;
                }

                if (installerDir.Exists)
                    installerDir.Delete(true);
                installerDir.Create();

                CopyFiles(internalInstallerDir, installerDir);

                // get path to our executable
                string myExecutablePath = Process.GetCurrentProcess().MainModule!.FileName;
                string rootDir = Path.GetDirectoryName(myExecutablePath)!;
                myExecutablePath = Path.Combine(rootDir, "MasterProject_A3_RJNL.exe");

                string updaterArgs =
                        $"onlyInstall " +
                        $"packagePath=\"{packageFile.FullName}\" " +
                        $"executablePath=\"{myExecutablePath}\" " +
                        $"appRootDirectory=\"{rootDir}\" " +
                        $"databaseKey=\"A3Games\"";

                FileInfo exeFile = new(Process.GetCurrentProcess().MainModule.FileName);
                string packagePath = packageFile.FullName;
                DirectoryInfo gameRootDir = new DirectoryInfo(Application.dataPath).Parent;

#if !UNITY_EDITOR

            Log.Push("Starting installer...");
            ProcessStartInfo installerStartInfo = new(installerPath.FullName);
            installerStartInfo.Arguments = updaterArgs;

            Process.Start(installerStartInfo);

            redis.Dispose();
            Log.Push("Closing game...");
            Process.GetCurrentProcess().Kill();
#endif

#if UNITY_EDITOR
                Log.Push("In Editor, playmode is disabled after downloading the app package due to otherwise " +
                    "having closed the editor and possibly corrupting its install, or the project files.\n\n");

                Log.Push($"using arguments: {updaterArgs}");
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            catch (Exception e)
            {
                Log.PushError(e.Message);
                Log.PushError(e.StackTrace);

                Windows.MessageBox($"Message: {e.Message}\n\n{e.StackTrace}");
            }
        }
        private void OnApplicationQuit()
        {
            redis?.Dispose();
            downloadTask?.Abort();
        }
        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (FileInfo file in source.GetFiles())
            {
                if (file.Extension == ".meta")
                    continue;
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }

            foreach (DirectoryInfo sourceSubDir in source.GetDirectories())
            {
                DirectoryInfo targetSubDir = target.CreateSubdirectory(sourceSubDir.Name);
                CopyFiles(sourceSubDir, targetSubDir);
            }
        }
        private async void DownloadUpdate()
        {
            await Task.Run(() =>
            {
                try
                {
                    var redisServer = new RedisConnection();
                    redisServer.MakeConnection("212.132.69.23", 6379);
                    redisServer.Authenticate("q.$p.I>%");
                    redisServer.ProgressReporter += progress =>
                    {
                        MainThread.Invoke(() =>
                        {
                            progressBar.progress = progress.Progress / 100;
                            progressText.text = $"Downloading... {System.MathF.Round(progress.Progress, 2)}%";
                        });
                    };

                    downloadedPackage = redisServer.Get<string, string>("A3Games::GameFiles");
                }
                catch (Exception e)
                {
                    hasFaulted = true;
                    return;
                }
                finally
                {
                    isDownloadComplete = true;
                }

            });
        }
    }
}