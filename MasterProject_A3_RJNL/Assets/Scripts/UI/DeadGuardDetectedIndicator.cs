using ShadowUprising.AI.Alarm;
using System.Collections;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.UI
{
    /// <summary>
    /// Component that listens for events from guards seeing a dead body and displays an alert on the UI if they do.
    /// </summary>
    public class DeadGuardDetectedIndicator : MonoBehaviour
    {
        AIDecisionHandler[] guards;

        [SerializeField] private GameObject alertIcon;
        [SerializeField] private TextBackgroundAnimator[] underlineImages;
        [SerializeField] private SimpleTextAnimator alertText;

        // Start is called before the first frame update
        void Start()
        {
            guards = FindObjectsOfType<AIDecisionHandler>();
            foreach (AIDecisionHandler guard in guards)
            {
                guard.onBodySpotted += OnBodySpotted;
            }

            alertIcon.SetActive(false);
        }

        public void TriggerAlert()
        {
            OnBodySpotted();
        }

        private void OnBodySpotted()
        {
            StartCoroutine(AnimateIconIn());
            underlineImages.Foreach(underline => underline.AnimateIn());
            alertText.StartWriting();
        }

        IEnumerator AnimateIconIn()
        {
            alertIcon.SetActive(true);
            alertIcon.transform.localScale = Vector3.zero;
            alertIcon.transform.localEulerAngles = Vector3.zero;
            float time = 0;
            while (time < 1)
            {
                time += Time.unscaledDeltaTime;
                alertIcon.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time);
                yield return null;
            }
        }

        IEnumerator AnimateIconOut()
        {
            alertIcon.SetActive(true);
            alertIcon.transform.localScale = Vector3.one;
            alertIcon.transform.localEulerAngles = Vector3.zero;
            float time = 0;
            while (time < 1)
            {
                time += Time.unscaledDeltaTime;
                alertIcon.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, time);
                yield return null;
            }
            alertIcon.SetActive(false);
        }
    }
}