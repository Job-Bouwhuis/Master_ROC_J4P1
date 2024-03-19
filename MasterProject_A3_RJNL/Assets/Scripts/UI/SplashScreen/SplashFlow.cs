//Creator: Job
using ShadowUprising.UI.Loading;
using System.Collections;
using UnityEngine;
using WinterRose;
using WinterRose.WIP.Redis;

namespace ShadowUprising.UI.SplashScreen
{
    /// <summary>
    /// The flow of the splash screen
    /// </summary>
    public class SplashFlow : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The duration of the total splash screen")]
        public float splashDuration = 5.0f;
        [Tooltip("The time at which the exit animation starts")]
        public float ExitAnimationStart = 4.0f;
        [Tooltip("The time when the logos start to increase to prepare to scale out again")]
        public float scaleIncreaseTime = 4.5f;
        [Tooltip("The time when the logos start to scale out")]
        public float scaleDecreaseTime = 4.9f;
        [Tooltip("The amount the scale increases by")]
        public float scaleIncreaseAmount = .15f;
        [Tooltip("The logos to animate")]
        public SplashLogo[] logos;

        [Header("Debug - DO NOT CHANGE")]
        [SerializeField] bool isloading = false;
        [SerializeField] bool exitAnimation = false;
        [SerializeField] float time = 0;

        private void Start()
        {
            Windows.MyHandle.Show();
            Windows.MyHandle.Focus();
            Windows.MyHandle.Maximize();
        }


        // Update is called once per frame
        void Update()
        {
            if (isloading) return;

            time += Time.deltaTime;


            if (time >= scaleDecreaseTime)
            {
                foreach (var logo in logos)
                {
                    logo.targetScale = 0.0f;
                }
            }
            else if (time >= scaleIncreaseTime && time < scaleDecreaseTime && !exitAnimation)
            {
                exitAnimation = true;
                foreach (var logo in logos)
                {
                    logo.targetScale *= 1.15f;
                }
            }
            Log.Push(UpdateChecker.Instance.ConnectingProcedureComplete);
            if(!UpdateChecker.Instance.ConnectingProcedureComplete)
            {
                time = 0;
            }

            if (time >= splashDuration && !isloading)
            {
                isloading = true;
                if(UpdateChecker.Instance.UpdateRequired)
                    LoadingScreen.Instance.LoadWithoutShow("AppUpdaterScene");
                else
                    LoadingScreen.Instance.LoadWithoutShow("MainMenu");

                foreach (var logo in logos)
                {
                    logo.targetScale = 0.0f;
                }
                enabled = false;
            }
        }
    }
}