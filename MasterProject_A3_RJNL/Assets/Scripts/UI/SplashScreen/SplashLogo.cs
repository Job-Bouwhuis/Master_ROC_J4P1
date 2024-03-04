using UnityEngine;

namespace ShadowUprising.UI.SplashScreen
{
    public class SplashLogo : MonoBehaviour
    {
        public float waitTime = 1.0f;
        public float targetScale = 1.0f;
        public float scaleSpeed = 1.0f;

        private float time;

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