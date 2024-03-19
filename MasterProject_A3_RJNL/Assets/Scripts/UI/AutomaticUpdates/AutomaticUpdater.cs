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
using System.Linq;

namespace ShadowUprising.AutoUpdates
{
    public class AutomaticUpdater : MonoBehaviour
    {
        public TMP_Text versionText;
        public TMP_Text logText;
        public TMP_Text progressText;

        public ProgressBar progressBar;

        RedisConnection redis;
        bool consoleWasOpen = false;
        bool isDownloadComplete = false;
        bool hasFaulted = false;

        Thread downloadTask;

        string downloadedPackage;

        // Start is called before the first frame update
        void Start()
        {
//#if UNITY_EDITOR
//            Windows.MessageBox("Hey there naughty boy, dont just go change the version file and start the game though the splash screen or even the updator scene while being in the editor.\n" +
//                "This would cause an update to happen which might corrupt the unity editor install.\n" +
//                "Luckily for you i have made this check to prevent this from happening. Thank me later.", "You naughty boy", Windows.MessageBoxButtons.OK, Windows.MessageBoxIcon.Exclamation);
//            UnityEditor.EditorApplication.isPlaying = false;
//            return;
//#endif

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

        void Log_OnLogPushed(Log.LogEventArgs args)
        {
            if (args.message is "Done")
                return;
            logText.text += args.message + "\n";
        }

        private void Update()
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

            downloadTask.Join();

            progressText.text = "Preparing to install...";

            var dir = new DirectoryInfo(Path.GetTempPath() + "A3Games");
            if (!dir.Exists)
                dir.Create();
            FileInfo fileInfo = new FileInfo(Path.GetTempPath() + "A3Games\\GameFiles.apkg");
            if (fileInfo.Exists)
                fileInfo.Delete();

            FileManager.Write(fileInfo.FullName, downloadedPackage);

            // TODO: do a quick install of the insta ller and start the installer to install the update. most insane sentence ive ever written.

            FileInfo installerPath = new(Path.GetTempPath() + "A3Games\\Installer.exe");
            string internalInstallerPath = Application.streamingAssetsPath + @"\AppInstaller\A3GamesInstaller.exe";
            if (!File.Exists(internalInstallerPath))
            {
                Log.PushError("Internal installer not found.");
                Log.PushWarning("Automatic updates will not be available. loading Main Menu so the game can still be played.");

                LoadingScreen.Instance.LoadWithoutShow("MainMenu");
                return;
            }

            if (File.Exists(installerPath.FullName))
                File.Delete(installerPath.FullName);

            File.Copy(internalInstallerPath, installerPath.FullName, true);


            // construct arguments for the installer

            /*
             installer arguments:
              - exePath: path to the exe of the game.
              - packagePath: path to the package the game downloaded.
              - gameRoot: path to the game's root directory.
             */

            FileInfo exeFile = new(Process.GetCurrentProcess().MainModule.FileName);
            Windows.MessageBox(exeFile.FullName);
            string packagePath = fileInfo.FullName;
            DirectoryInfo gameRootDir = new DirectoryInfo(Application.dataPath).Parent;

            //string installerFolder = FileManager.PathOneUp(installerPath.FullName);

            //// due to my lack of coding skills before submitting the WinterRose library, i have to correct a mistake i made in this function.
            //// path example: C:\Users\Job\Documents\A3Games\Installer.exe
            //// the function would return C:Users\Job\Documents\A3Games
            //// while the expected result is C:\Users\Job\Documents\A3Games
            //// so i have to insert a backslash at the 2nd index of the string.
            //// im too lazy to reimport the library and fix the function so i will just do this.
            //installerFolder = installerFolder.Insert(2, "\\");
            //FileInfo argumentsFile = new(installerFolder + "\\installerargs.iargs"); // name of the file is required to be this. the installer will look for this file in the same directory as the installer.

#if !UNITY_EDITOR

            Log.Push("Starting installer...");
            ProcessStartInfo installerStartInfo = new(installerPath.FullName);
            installerStartInfo.Arguments = $"exePath=\"{exeFile.FullName}\" packagePath=\"{packagePath}\" gameRoot=\"{gameRootDir.FullName}\"";

            Process.Start(installerStartInfo);

            redis.Dispose();
            Log.Push("Closing game...");
            Process.GetCurrentProcess().Kill();
#endif

#if UNITY_EDITOR
            Log.Push("In Editor, playmode is disabled after downloading the app package due to otherwise having closed the editor and possibly corrupting its install, or the project files.");
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void OnApplicationQuit()
        {
            redis?.Dispose();
            downloadTask?.Abort();
        }


        async void DownloadUpdate()
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