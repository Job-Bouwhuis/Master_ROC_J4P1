// Creator: Job

using System;
using UnityEngine;
using WinterRose;
using static UnityEngine.Rendering.DebugUI;

namespace ShadowUprising.UI
{
    /// <summary>
    /// A simple class to display the value of a slider in a text element.
    /// </summary>
    public class SliderValueDisplay : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI text;
        public UnityEngine.UI.Slider slider;

        void Start()
        {
            float minValue = slider.minValue;
            float maxValue = slider.maxValue;
            float value = slider.value;

            float percentage = (value - minValue) / (maxValue - minValue);


            text.text = (percentage * 100).FloorToInt().ToString();
            slider.onValueChanged.AddListener((value) =>
            {
                float minValue = slider.minValue;
                float maxValue = slider.maxValue;

                float percentage = (value - minValue) / (maxValue - minValue);

                text.text = (percentage * 100).FloorToInt().ToString();
            });
        }
    }
}