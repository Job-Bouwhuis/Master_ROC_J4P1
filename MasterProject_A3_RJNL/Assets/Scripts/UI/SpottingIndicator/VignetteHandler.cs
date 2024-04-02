//Creator: Job
using ShadowUprising.UI.Loading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ShadowUprising.UI
{
    /// <summary>
    /// Vignette control handler
    /// </summary>
    [RequireComponent(typeof(Volume))]
    public class VignetteHandler : MonoBehaviour
    {
        [Header("Intensity Config")]
        [Tooltip("The maximum intensity it may go")]
        public float maxIntensity = 1.0f;
        [Tooltip("The minimum intensity it may go")]
        public float minIntensity = 0.0f;
        [Tooltip("The speed at which the vignette changes intensity")]
        public float speed = 1.0f;

        [Header("Color Config")]
        [Tooltip("The color at the minimum intensity")]
        public Color colorAtMinIntensity = Color.black;
        [Tooltip("The color at the maximum intensity")]
        public Color colorAtMaxIntensity = Color.red;

        [Header("Debug - DO NOT CHANGE")]
        [SerializeField] private Volume volume;
        [SerializeField] private Color currentColor;
        [SerializeField] private float targetIntensity = 0.0f;
        [SerializeField] private float intensity = 0.0f;
        [SerializeField] private float colorPercentage = 0.0f;

        /// <summary>
        /// The current percentage of the target intensity between the min and max intensity<br></br>
        /// A Value between 0 and 1. 0 being the min intensity and 1 being the max intensity
        /// </summary>
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
        private void Update()
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
}