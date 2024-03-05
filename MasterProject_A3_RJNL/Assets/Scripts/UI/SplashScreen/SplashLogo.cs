//Creator: Job
using UnityEngine;

namespace ShadowUprising.UI.SplashScreen
{
    /// <summary>
    /// A component to animate a logo on the splashscreen
    /// </summary>
    public class SplashLogo : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The time the logo will wait before scaling in")]
        public float waitTime = 1.0f;
        [Tooltip("The target scale of the logo")]
        public float targetScale = 1.0f;
        [Tooltip("The speed at which the logo will scale")]
        public float scaleSpeed = 1.0f;

        [Header("Debug - DO NOT CHANGE")]
        [SerializeField] private float time;

        private void Awake() => transform.localScale = Vector3.zero;

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (time < waitTime) return;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * targetScale, Time.deltaTime * scaleSpeed);
        }
    }
}