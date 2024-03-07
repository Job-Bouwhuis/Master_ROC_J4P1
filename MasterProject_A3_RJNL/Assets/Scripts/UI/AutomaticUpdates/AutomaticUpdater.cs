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

namespace ShadowUprising.AutoUpdates
{
    public class AutomaticUpdater : MonoBehaviour
    {
        public TMP_Text versionText;
        public TMP_Text logText;

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
                redis.ProgressReporter += progress =>
                {
                    progressBar.progress = progress;
                    Log.Push($"Downloading update... {progress * 100}%");
                };
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

            var dir = new DirectoryInfo(Path.GetTempPath() + "\\A3Games");
            if (!dir.Exists)
                dir.Create();
            FileInfo fileInfo = new FileInfo(Path.GetTempPath() + "\\A3Games\\GameFiles.apkg");
            if (fileInfo.Exists)
                fileInfo.Delete();

            FileManager.Write(fileInfo.FullName, downloadedPackage);

            // TODO: do a quick install of the installer and start the installer to install the update. most insane sentence ive ever written.
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
                            progressBar.progress = progress;
                            Log.Push($"Downloading update... {progress * 100}%");
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