// Creator: Job

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

        // Start is called before the first frame update
        void Start()
        {
            text.text = (slider.value * 100).FloorToInt().ToString();
            slider.onValueChanged.AddListener((value) => text.text = (value * 100).FloorToInt().ToString());
        }
    }
}