//Creator: Job
using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace ShadowUprising.UI.SpottingIndicator
{
    /// <summary>
    /// This class is used to handle when the player is detected by an enemy, 
    /// and will change the vignette to indicate that the play is being detected and then when the player is detected
    /// </summary>
    [RequireComponent(typeof(VignetteHandler))]
    public class DetectionIndicator : MonoBehaviour
    {
        [Header("Functionality")]
        [Tooltip("Whether or not the player is currently in the process of being detected")]
        public bool isDetecting = false;
        [Tooltip("Whether or not the player is currently detected")]
        public bool isDetected = false;

        [Header("Settings")]
        [Tooltip("The speed at which the detection process occurs in seconds")]
        public float detectionSpeed = 0.1f;
        [Tooltip("The speed at which the vignette fades out when the player is detected")]
        public float vignetteFadeoutWhenDetected = 50f;

        [Header("Debug - DO NOT EDIT")]
        [SerializeField] private VignetteHandler vignette;
        [SerializeField] private Color vignetteDefaultColor;
        [SerializeField] private bool lastIsDetected = false;

        void Awake()
        {
            vignette = GetComponent<VignetteHandler>();

            // For the loading screen, we want to stop the detection process, so that the Vignette handler can reset itself when the loading screen is activated
            if (LoadingScreen.Instance != null)
            {
                LoadingScreen.Instance.OnStartLoading.Subscribe(() =>
                {
                    isDetected = false;
                    isDetecting = false;
                    vignette.colorAtMinIntensity = vignetteDefaultColor;

                    return 0;
                });
            }
        }
        void Update()
        {
            if(isDetected && isDetecting)
            {
                isDetecting = false;
            }

            if (isDetecting)
            {
                StopAllCoroutines();
                // detectionSpeed is in seconds. 1 / detectionSpeed is the number of frames it takes to go from 0 to 1
                vignette.TargetPercentage += Time.deltaTime * (1 / detectionSpeed);
                if (vignette.TargetPercentage >= 0.97)
                    vignette.TargetPercentage = 1.0f;

                if (vignette.TargetPercentage >= 1.0f)
                {
                    isDetecting = false;
                    isDetected = true;
                }
            }

            if (isDetected)
            {
                // set the vignette min color to the max color and cache the default color
                vignette.colorAtMinIntensity = vignette.colorAtMaxIntensity;

                // lerp the vignette back to min intensity
                vignette.TargetPercentage = Mathf.Lerp(vignette.TargetPercentage, 0, Time.deltaTime * vignetteFadeoutWhenDetected);
            }

            if (isDetected != lastIsDetected && !isDetected)
            {
                StopAllCoroutines();
                StartCoroutine(ResetVignetteColor());
            }

            if(!isDetected && !isDetecting && vignette.TargetPercentage > 0)
            {
                // lerp the vignette back to min intensity
                vignette.TargetPercentage = Mathf.Lerp(vignette.TargetPercentage, 0, Time.deltaTime * vignetteFadeoutWhenDetected);
            }

            lastIsDetected = isDetected;
        }
        private IEnumerator ResetVignetteColor()
        {
            while (vignette.colorAtMinIntensity != vignetteDefaultColor)
            {
                vignette.colorAtMinIntensity = Color.Lerp(vignette.colorAtMinIntensity, vignetteDefaultColor, Time.deltaTime * 2);
                yield return null;
            }
        }
    }
}