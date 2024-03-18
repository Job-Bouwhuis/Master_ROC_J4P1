// Creator: Job
using System.Collections;
using TMPro;
using UnityEngine;

namespace ShadowUprising.UI.SplashScreen
{
    /// <summary>
    /// Component to animate the splash text on the splash screen.
    /// </summary>
    public class SplashTextAnimator : MonoBehaviour
    {
        [Header("Settings")]
        //[Tooltip("The time the text will wait before showing")]
        //public float textWaitTime = 1f;
        [Tooltip("The speed at which the text will change")]
        public float textChangeSpeed = 0.5f;
        [Tooltip("The time the text will stay on screen before going away again")]
        public float textOnScreenTime = 2f;
        [Tooltip("The text to display")]
        public string textToDisplay;

        [Header("Debug - DO NOT CHANGE")]
        TMP_Text text;

        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<TMP_Text>();
            StartCoroutine(AnimateText());
            text.text = "";
        }

        private IEnumerator AnimateText()
        {
#if UNITY_EDITOR
            yield return new WaitForSecondsRealtime(1);
#else
            yield return new WaitForSecondsRealtime(0.5f);
#endif

            Log.Push("animating text in");

            for (int i = 0; i < textToDisplay.Length; i++)
            {
                text.text += textToDisplay[i];
                yield return new WaitForSecondsRealtime(textChangeSpeed);
            }

            yield return new WaitForSecondsRealtime(textOnScreenTime);
            Log.Push("animating text out");

            for (int i = 0; i < textToDisplay.Length; i++)
            {
                text.text = text.text.Remove(text.text.Length - 1);
                yield return new WaitForSecondsRealtime(textChangeSpeed);
            }
        }
    }
}