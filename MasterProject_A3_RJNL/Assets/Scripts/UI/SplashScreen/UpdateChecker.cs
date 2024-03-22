// Creator: Job
using System.Threading.Tasks;
using UnityEngine;
using WinterRose.WIP.Redis;
using ShadowUprising.UnityUtils;
using ShadowUprising;

namespace ShadowUprising.AutoUpdates
{
    /// <summary>
    /// Class responsible for checking if the game needs an update.
    /// </summary>
    public class UpdateChecker : Singleton<UpdateChecker>
    {
        private RedisConnection redis;

        /// <summary>
        /// Whether the process of connecting to the database has been completed.
        /// </summary>
        public bool ConnectingProcedureComplete { get; private set; } = false;
        /// <summary>
        /// Whether the game is connected to the database.
        /// </summary>
        public bool IsConnected => redis.IsConnected;

        /// <summary>
        /// Whether the game needs an update.
        /// </summary>
        public bool UpdateRequired { get; private set; } = false;
        [SerializeField] bool completed = false;

        protected override void Awake()
        {
            base.Awake();
            _ = ConnectToRedis();
        }
        void Update()
        {
            if (ConnectingProcedureComplete && !completed)
            {
                completed = true;
                string value = redis.GetHashFieldValue("A3Games::GameData", "Version");
                string current = Resources.Load<TextAsset>("Version/GameVersion").text;

                // i know this is very complicated for a simple comparison, but doing it normally strangely doesnt work.
                // this does work, so lets keep it.
                bool isUpdateRequired = new System.Data.DataTable().Compute(current + " < " + value, null).ToString() is "True";

                if (isUpdateRequired)
                    UpdateRequired = true;

                redis.Dispose();
                Log.Push("Terminated connection to database.");
            }
        }
        private async Task ConnectToRedis()
        {
            Log.Push("Connecting to Database...");
            await Task.Run(() =>
            {
                redis = new RedisConnection();
                try
                {
                    redis.MakeConnection("212.132.69.23", 6379);
                    redis.Authenticate("q.$p.I>%");
                }
                catch { } // ignore any exceptions,
                          // if they occur, the process of connecting to the server has fauled,
                          // and the game will treat it as no update required.
                ConnectingProcedureComplete = true;
            });
        }
    }
}