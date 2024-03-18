using System.Collections;
using WinterRose;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using WinterRose.WIP.Redis;
using ShadowUprising.Settings;
using ShadowUprising.UnityUtils;

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
            string value = redis.Get<string, string>("A3MasterProjectVersion");
            string current = Resources.Load<TextAsset>("Version/GameVersion").text;

            bool isUpdateRequired = new System.Data.DataTable().Compute(current + " < " + value, null).ToString() is "True";

            if(isUpdateRequired)
                UpdateRequired = true;

            text.text = "";
        }
    }

    private async Task ConnectToRedis()
    {
        await Task.Run(() =>
        {
            redis = new RedisConnection();
            try
            {
                redis.MakeConnection("212.132.69.23", 6379);
                redis.Authenticate("q.$p.I>%");
            }
            catch
            {
 
            }
            ConnectingProcedureComplete = true;
        });
    }
}
