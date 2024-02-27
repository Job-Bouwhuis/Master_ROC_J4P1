// Creator: Job
using UnityEngine;

namespace ShadowUprising.UI.Loading
{
    public class LoadingBar : MonoBehaviour
    {
        public float smoothSpeed = 5;
        public float progress = 0;
        public float visualProgress = 0;

        public float maxWidth;
        public float minWidth = 0;

        new private RectTransform transform;

        private void Awake()
        {
            transform = base.transform as RectTransform;

            maxWidth = transform.sizeDelta.x;
        }

        // Update is called once per frame
        void Update()
        {
            visualProgress = Mathf.Lerp(visualProgress, progress, smoothSpeed * Time.deltaTime);
            transform.sizeDelta = new Vector2(Mathf.Lerp(minWidth, maxWidth, visualProgress), transform.sizeDelta.y);
        }
    }
}