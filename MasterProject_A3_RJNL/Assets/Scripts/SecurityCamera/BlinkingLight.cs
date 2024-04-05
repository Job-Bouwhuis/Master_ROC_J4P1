// Creator: Job
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace ShadowUprising.Cameras
{
    /// <summary>
    /// Component that makes a light blink on and off by manimulating the emission color of the material
    /// </summary>
    public class BlinkingLight : MonoBehaviour
    {
        private const byte MAX_BYTE_FOR_OVEREXPOSED_COLOR = 191;

        [Header("Light Settings")]
        [SerializeField, Range(0, 10)]
        private float onIntensity = 5;
        [SerializeField, Range(0, 10)]
        private float offIntensity = 0;

        [SerializeField, Range(0, 10)]
        private float onTime = 1;
        [SerializeField, Range(0, 10)]
        private float offTime = 1;

        [SerializeField, Header("DEBUG - DO NOT CHANGE")]
        private float time;

        [SerializeField]
        Material lightMaterial;

        [SerializeField]
        private bool isOff = true;
        private bool switching = false;

        private void SetIntensity(float intensity)
        {
            Color originalEmissionColor = GetComponent<Renderer>().material.GetColor("_EmissionColor");
            float scaleFactor = Mathf.Pow(2f, intensity) * 255f / MAX_BYTE_FOR_OVEREXPOSED_COLOR;
            float maxColorComponent = 255f / scaleFactor;
            float ratio = maxColorComponent / Mathf.Max(originalEmissionColor.r, originalEmissionColor.g, originalEmissionColor.b);

            Color newEmissionColor = new Color(
                originalEmissionColor.r * ratio,
                originalEmissionColor.g * ratio,
                originalEmissionColor.b * ratio
            );

            // Apply the adjusted emission color to the material
            GetComponent<Renderer>().material.SetColor("_EmissionColor", newEmissionColor);
        }

        // Start is called before the first frame update
        void Start()
        {
            // copy the material so we dont change the original, and apply it to the renderer that is attached to this object
            lightMaterial = GetComponent<Renderer>().material;

            // set a random time so that the lights dont blink at the same time
            time = Random.Range(0, onTime + offTime);
        }

        // Update is called once per frame
        void Update()
        {
            if (isOff)
            {
                if (switching)
                {
                    SetIntensity(offIntensity);
                    switching = false;
                }

                if (time >= onTime)
                {
                    isOff = false;
                    time = 0;
                    switching = true;
                }
            }
            else
            {
                if (switching)
                {
                    SetIntensity(onIntensity);
                    switching = false;
                }

                if (time >= offTime)
                {
                    isOff = true;
                    time = 0;
                    switching = true;
                }
            }

            time += Time.deltaTime;
        }
    }
}