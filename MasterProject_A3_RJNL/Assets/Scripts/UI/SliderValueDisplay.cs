using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WinterRose;

public class SliderValueDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public UnityEngine.UI.Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        text.text = slider.value.ToString();
        slider.onValueChanged.AddListener((float value) => text.text = (value * 100).FloorToInt().ToString());
    }
}
