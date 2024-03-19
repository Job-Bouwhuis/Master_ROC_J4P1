using System.Collections;
using WinterRose;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using WinterRose.WIP.Redis;
using ShadowUprising.Settings;
using ShadowUprising.UnityUtils;
using ShadowUprising;

public class UpdateChecker : Singleton<UpdateChecker>
{
    private RedisConnection redis;
    private TMP_Text text;

    public bool ConnectingProcedureComplete { get; private set; } = false;
    public bool IsConnected => redis.IsConnected;

    public bool UpdateRequired { get; private set; } = false;
    [SerializeField] bool completed = false;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        _ = ConnectToRedis();
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ConnectingProcedureComplete && !completed)
        {
            completed = true;
            text.text = "Checking for updates....";
            string value = redis.GetHashFieldValue("A3Games::GameData", "Version");
            string current = Resources.Load<TextAsset>("Version/GameVersion").text;

            // i know this is very complicated for a simple comparison, but doing it normally strangely doesnt work.
            // this does work, so lets keep it.
            bool isUpdateRequired = new System.Data.DataTable().Compute(current + " < " + value, null).ToString() is "True";

            if(isUpdateRequired)
                UpdateRequired = true;

            text.text = "";

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
