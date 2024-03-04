using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(VignetteHandler))]
public class DetectionIndicator : MonoBehaviour
{
    [Header("Functionality")]
    public bool isDetecting = false;
    public bool isDetected = false;

    [Header("Settings")]
    public float detectionSpeed = 0.1f;
    public float vignetteFadeoutWhenDetected = 50f;

    [Header("References - DO NOT EDIT")]
    [SerializeField] private VignetteHandler vignette;
    [SerializeField] private Color vignetteDefaultColor;
    [SerializeField] private bool lastIsDetected = false;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
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
