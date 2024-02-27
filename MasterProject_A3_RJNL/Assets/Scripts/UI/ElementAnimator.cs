using ShadowUprising;
using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.UI.InGame
{
    public class ElementAnimator : MonoBehaviour
    {
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

        private float timeOnScreen = 0;

        public bool IsVisible => Vector3.Distance(transform.localPosition, visiblePosition) < 0.01f;

        public void Show()
        {
            StopAllCoroutines();
            StartCoroutine(AnimateElementIn());
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
            StopAllCoroutines();
            OnScreenIndefinitely = false;
        }

        /// <summary>
        /// Animates the element into the visible position
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimateElementIn()
        {
            while (Vector3.Distance(transform.localPosition, visiblePosition) > 0.01f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, visiblePosition, animationSpeed * Time.deltaTime);
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
            while (Vector3.Distance(transform.localPosition, hiddenPosition) > 0.01f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, hiddenPosition, animationSpeed * Time.deltaTime);
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

            if (!IsVisible)
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