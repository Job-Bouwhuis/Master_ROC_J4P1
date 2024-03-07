// Creator: Job
using System.Diagnostics;
using UnityEngine;

namespace ShadowUprising.UI.Loading
{
    /// <summary>
    /// A progress bar that can be used to show the progress of a task.
    /// </summary>
    [ExecuteAlways]
    public class ProgressBar : MonoBehaviour
    {
        /// <summary>
        /// The speed at which the visual progress will move towards the actual progress.
        /// </summary>
        public float smoothSpeed = 5;
        /// <summary>
        /// The progress of the bar. This should be a value between 0 and 1.
        /// </summary>
        public float progress = 0;
        [HideInInspector, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public float visualProgress = 0;

        private float maxWidth;
        private float minWidth = 0;

        new private RectTransform transform;

        private void Awake()
        {
            transform = base.transform as RectTransform;

            maxWidth = transform.sizeDelta.x;
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                transform.sizeDelta = new Vector2(Mathf.Lerp(minWidth, maxWidth, progress), transform.sizeDelta.y);
                return;
            }
#endif
            visualProgress = Mathf.Lerp(visualProgress, progress, smoothSpeed * Time.deltaTime);
            transform.sizeDelta = new Vector2(Mathf.Lerp(minWidth, maxWidth, visualProgress), transform.sizeDelta.y);
        }
    }
}