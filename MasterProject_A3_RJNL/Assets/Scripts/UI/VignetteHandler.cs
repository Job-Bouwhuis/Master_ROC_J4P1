using ShadowUprising;
using ShadowUprising.UI.Loading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using WinterRose;

[RequireComponent(typeof(Volume))]
public class VignetteHandler : MonoBehaviour
{
    [Header("Intensity Config")]
    public float maxIntensity = 1.0f;
    public float minIntensity = 0.0f;
    public float speed = 1.0f;

    [Header("Color Config")]
    public Color colorAtMinIntensity = Color.black;
    public Color colorAtMaxIntensity = Color.red;

    [Header("Debug - DO NOT CHANGE")]
    [SerializeField] private Volume volume;
    [SerializeField] private Color currentColor;
    [SerializeField] private float targetIntensity = 0.0f;
    [SerializeField] private float intensity = 0.0f;
    [SerializeField] private float colorPercentage = 0.0f;


    public float TargetPercentage
    {
        get
        {
            return targetPercentage;
        }
        set
        {
            targetPercentage = Mathf.Clamp(value, 0.0f, 1.0f);
            targetIntensity = Mathf.Lerp(minIntensity, maxIntensity, targetPercentage);
        }
    }
    private float targetPercentage = 0.0f;

    private void Awake()
    {
        targetIntensity = minIntensity;
        intensity = 0;
        volume = GetComponent<Volume>();
    }

    private void Start()
    {
        // This code is in start because we want to make sure that any users of the VignetteHandler
        // have a chance to subscribe on the loading start event and stop their interaction with the VignetteHandler
        if (LoadingScreen.Instance != null)
        {
            LoadingScreen.Instance.OnStartLoading.Subscribe(() =>
            {
                targetIntensity = minIntensity;
                return 0;
            });
        }
    }

    public void Update()
    {
        float lerp = Mathf.Lerp(intensity, targetIntensity, speed * Time.deltaTime);
        lerp = Mathf.Clamp(lerp, minIntensity, maxIntensity);
        intensity = lerp;

        if (intensity < 0.0f)
        {
            intensity = 0.0f;
        }
        else if (intensity > 1.0f)
        {
            intensity = 1.0f;
        }

        SetVignetteIntensity(intensity);


        colorPercentage = (intensity - minIntensity) / (maxIntensity - minIntensity);
        colorPercentage = Mathf.Clamp(colorPercentage, 0.0f, 1.0f);


        currentColor = Color.Lerp(colorAtMinIntensity, colorAtMaxIntensity, colorPercentage);
        SetVignetteColor(currentColor);
    }

    private void SetVignetteIntensity(float intensity)
    {
        if (volume.profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.value = intensity;
        }
    }

    private void SetVignetteColor(Color color)
    {
        if (volume.profile.TryGet(out Vignette vignette))
        {
            vignette.color.value = color;
        }
    }
}
