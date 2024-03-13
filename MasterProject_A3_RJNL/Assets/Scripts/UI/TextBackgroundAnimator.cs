using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShadowUprising.UI
{
    /// <summary>
    /// Allows for an image to be animated specifically to serve as a background for text.
    /// </summary>
    public class TextBackgroundAnimator : MonoBehaviour
    {
        [Header("Settings")]
        public float animationSpeed = 1.0f;
        public bool animateOnStart = true;
        public float WaitBeforeAnimationStart = 0;

        [Header("Debug")]
        [SerializeField] private float startWidth = 0.0f;
        [SerializeField] private Image image;

        /// <summary>
        /// When the animation starts, this event is called. passed true if the animation is the entry animation, false if it is the exit animation.
        /// </summary>
        public event Action<bool> OnAnimationStart = delegate { };

        private new RectTransform transform;

        // Start is called before the first frame update
        void Start()
        {
            transform = base.transform as RectTransform;
            image = GetComponent<Image>();
            startWidth = transform.sizeDelta.x;

            transform.sizeDelta = new Vector2(0.0f, transform.sizeDelta.y);
            image.gameObject.SetActive(true);

            if(animateOnStart)
            {
                StopAllCoroutines();
                AnimateIn();
            }
        }

        public void AnimateIn()
        {
            StopAllCoroutines();
            OnAnimationStart(true);
            StartCoroutine(AnimateInCoroutine());
        }

        private IEnumerator AnimateInCoroutine()
        {
            yield return new WaitForSecondsRealtime(WaitBeforeAnimationStart);

            while(transform.sizeDelta.x < startWidth)
            {
                transform.sizeDelta = new Vector2(transform.sizeDelta.x + (Time.unscaledDeltaTime * animationSpeed), transform.sizeDelta.y);
                yield return null;
            }
        }

        public void AnimateOut()
        {
            StopAllCoroutines();
            OnAnimationStart(false);
            StartCoroutine(AnimateOutCoroutine());
        }

        private IEnumerator AnimateOutCoroutine()
        {
            yield return new WaitForSecondsRealtime(WaitBeforeAnimationStart);

            while(transform.sizeDelta.x > 0)
            {
                transform.sizeDelta = new Vector2(transform.sizeDelta.x - (Time.unscaledDeltaTime * animationSpeed), transform.sizeDelta.y);
                yield return null;
            }
        }
    }
}