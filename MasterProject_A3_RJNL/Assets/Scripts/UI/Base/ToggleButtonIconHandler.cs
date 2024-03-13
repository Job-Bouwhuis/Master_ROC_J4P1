using ShadowUprising;
using ShadowUprising.UI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ToggleButtonIconHandler : MonoBehaviour
{
    [Header("References")]
    public Sprite EnabledSprite;
    public Sprite DisabledSprite;
    public TextButton button;

    private Image _image;
    private bool lastButtonToggleState;

    // Start is called before the first frame update
    void Start() 
    { 
        _image = GetComponent<Image>();
        button = GetComponentInParent<TextButton>();

        if(!button.isToggle)
        {
            Log.Push("Button is not in toggle mode. This script will be disabled, please use a different script");
            return;
        }

        lastButtonToggleState = !button.toggleState;
    }

    // Update is called once per frame
    void Update()
    {
        if(button.toggleState != lastButtonToggleState)
        {
            if(button.toggleState)
            {
                _image.sprite = EnabledSprite;
            }
            else
            {
                _image.sprite = DisabledSprite;
            }
        }

        lastButtonToggleState = button.toggleState;

    }
}