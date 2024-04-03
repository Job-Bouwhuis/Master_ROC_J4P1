// Creator: Job
using System.Diagnostics;
using UnityEngine;

namespace ShadowUprising.UI.Loading
{
    /// <summary>
    /// A progress bar that can be used to show the progress of a task.
    /// </summary>
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
        [Tooltip("If true, the progress will be updated using unscaled time. This is useful for loading screens.")]
        public bool useUnscaledTime = false;
        [HideInInspector, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public float visualProgress = 0;

        private float maxWidth;
        private float minWidth = 0;

        new private RectTransform transform;

        private float time => useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

        private void Awake()
        {
            transform = base.transform as RectTransform;

            maxWidth = transform.sizeDelta.x;
        }

        // Update is called once per frame
        void Update()
        {
            visualProgress = Mathf.Lerp(visualProgress, progress, smoothSpeed * time);
            transform.sizeDelta = new Vector2(Mathf.Lerp(minWidth, maxWidth, visualProgress), transform.sizeDelta.y);
        }
    }
}