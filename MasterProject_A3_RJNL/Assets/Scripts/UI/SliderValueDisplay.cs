// Creator: Job

using UnityEngine;
using WinterRose;

namespace ShadowUprising.UI
{
    /// <summary>
    /// A simple class to display the value of a slider in a text element.
    /// </summary>
    public class SliderValueDisplay : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI text;
        public UnityEngine.UI.Slider slider;

        // Start is called before the first frame update
        void Start()
        {
            text.text = slider.value.ToString();
            slider.onValueChanged.AddListener((value) => text.text = (value * 100).FloorToInt().ToString());
        }
    }
}