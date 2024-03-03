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

        private bool ShouldBeVisible
        {
            get => shouldBeVisible;
            set
            {
                shouldBeVisible = value;
                    OnAnimationStart(value ? AnimationType.Enter : AnimationType.Exit);
            }
        }
        private bool shouldBeVisible = false;

        /// <summary>
        /// A private shortcut to the time variable that returns the unscaled time if <see cref="useUnscaledTime"/> is true
        /// </summary>
        private float time
        {
            get
            {
                if (useUnscaledTime)
                    return Time.timeScale is 1 ? Time.deltaTime : Time.unscaledDeltaTime;
                else
                    return Time.deltaTime;
            }
        }

        public void Show()
        {
            shouldBeVisible = true;
            timeOnScreen = 0;
        }

        public void Hide()
        {
            shouldBeVisible = false;
        }

        public void ShowIndefinite()
        {
            OnScreenIndefinitely = true;
            Show();
        }

        public void HideFromIndefinite()
        {
            OnScreenIndefinitely = false;
        }


        /// <summary>
        /// Animates the element into the visible position
        /// </summary>
        /// <returns></returns>
        public bool AnimateElementIn()
        {
            if (Vector3.Distance(transform.localPosition, visiblePosition) < 0.01f)
            {
                transform.localPosition = visiblePosition;
                return true;
            }


            transform.localPosition = Vector3.Lerp(transform.localPosition, visiblePosition, animationSpeed * time);
            return false;
        }

        /// <summary>
        /// Animates the element out to the hidden position
        /// </summary>
        /// <returns></returns>
        public bool AnimateElementOut()
        {
            if (Vector3.Distance(transform.localPosition, hiddenPosition) < 0.01f)
            {
                transform.localPosition = hiddenPosition;
                return true;
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, hiddenPosition, animationSpeed * time);
            return false;
        }

        void Start()
        {
            transform.localPosition = hiddenPosition;

            if (LoadingScreen.Instance == null)
                return;


            LoadingScreen.Instance.OnLoadingComplete.AddListener(() =>
            {
                if (!HideOnStart)
                    shouldBeVisible = true;

                LoadingScreen.Instance.OnStartLoading += () =>
                {
                    shouldBeVisible = false;
                    return delayLoading;
                };
            });
        }

        private void Update()
        {
            if (LoadingScreen.Instance is not null and { IsLoading: true })
            {
                if (AnimateElementOut())
                    return;
            }

            if (ShouldBeVisible)
                AnimateElementIn();
            else
                AnimateElementOut();


            if (!IsAtVisiblePosition)
                return;

            if (OnScreenIndefinitely)
            {
                timeOnScreen = 0;
                return;
            }
            timeOnScreen += time;



            if (timeOnScreen >= TimeOnScreenBeforeHide)
            {
                timeOnScreen = 0;
                shouldBeVisible = false;
            }

        }
    }
}