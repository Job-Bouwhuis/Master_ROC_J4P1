using ShadowUprising;
using ShadowUprising.UI.Loading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.UI
{
    public class ElementAnimator : MonoBehaviour
    {
        public enum AnimationType
        {
            Enter,
            Exit
        }

        [Tooltip("The position where the element will be when it is visible")]
        public Vector3 visiblePosition;
        [Tooltip("The position where the element will be when it is hidden")]
        public Vector3 hiddenPosition;
        [Tooltip("The time it takes to animate the element")]
        public float animationSpeed = 1.0f;
        [Tooltip("The minimum amount of time the loading screen will wait until it will start")]
        public float delayLoading = 0.5f;
        [Tooltip("The time the element will stay on screen before hiding")]
        public float TimeOnScreenBeforeHide = 1.5f;
        [Tooltip("If true, the element will stay on screen indefinitely so long this value is true")]
        public bool OnScreenIndefinitely = false;
        [Tooltip("Whether or not the animator should use scaled time or unscaled time")]
        public bool useUnscaledTime = false;
        [Tooltip("Whether or not the element should be hidden on start")]
        public bool HideOnStart = false;
        private float timeOnScreen = 0;

        public Action<AnimationType> OnAnimationStart = delegate { };

        /// <summary>
        /// Whether or not the element is in its visible position
        /// </summary>
        public bool IsAtVisiblePosition => Vector3.Distance(transform.localPosition, visiblePosition) < 0.01f;

        public void Show()
        {
            StopAllCoroutines();
            StartCoroutine(AnimateElementIn());
            timeOnScreen = 0;
        }

        public void Hide()
        {
            StopAllCoroutines();
            StartCoroutine(AnimateElementOut());
        }

        public void ShowIndefinite()
        {
            OnScreenIndefinitely = true;
            Show();
        }

        public void HideFromIndefinite()
        {
            if (!IsAtVisiblePosition)
            {
                StartCoroutine(SetHideFromIndefinite());
                return;
            }

            StopAllCoroutines();
            OnScreenIndefinitely = false;
        }

        private IEnumerator SetHideFromIndefinite()
        {
            while (!IsAtVisiblePosition)
            {
                yield return null;
            }

            StopAllCoroutines();
            OnScreenIndefinitely = false;
        }

        /// <summary>
        /// Animates the element into the visible position
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimateElementIn()
        {
            OnAnimationStart(AnimationType.Enter);

            float time = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

            while (Vector3.Distance(transform.localPosition, visiblePosition) > 0.01f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, visiblePosition, animationSpeed * time);
                yield return null;
            }

            transform.localPosition = visiblePosition;
        }

        /// <summary>
        /// Animates the element out to the hidden position
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimateElementOut()
        {
            OnAnimationStart(AnimationType.Exit);

            float time = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            while (Vector3.Distance(transform.localPosition, hiddenPosition) > 0.01f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, hiddenPosition, animationSpeed * time);
                yield return null;
            }

            transform.localPosition = hiddenPosition;
        }

        void Start()
        {
            if (LoadingScreen.Instance is null)
            {
                Log.PushError("LoadingScreen is null. please start the game from the main menu to get this.");

                return;
            }

            transform.localPosition = hiddenPosition;

            LoadingScreen.Instance.OnLoadingComplete.AddListener(() =>
            {
                if (!HideOnStart)
                    StartCoroutine(AnimateElementIn());


                LoadingScreen.Instance.OnStartLoading += () =>
                {
                    StartCoroutine(AnimateElementOut());

                    return delayLoading;
                };
            });
        }

        private void Update()
        {
            if (LoadingScreen.Instance is not null and { IsLoading: true })
                return;

            if (!IsAtVisiblePosition)
                return;

            if (OnScreenIndefinitely)
            {
                timeOnScreen = 0;
                return;
            }
            timeOnScreen += Time.deltaTime;



            if (timeOnScreen >= TimeOnScreenBeforeHide)
            {
                timeOnScreen = 0;
                StartCoroutine(AnimateElementOut());
            }

        }
    }
}