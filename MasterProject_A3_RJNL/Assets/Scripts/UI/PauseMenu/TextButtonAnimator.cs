using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ShadowUprising.UI.PauseMenu
{
    /// <summary>
    /// Animates the <see cref="TextButton"/> to appear and disappear in a fancy way
    /// </summary>
    [RequireComponent(typeof(TextButton))]
    public class TextButtonAnimator : MonoBehaviour
    {
        /// <summary>
        /// The start position of the button
        /// </summary>
        [Header("Start Position")]
        public Vector3 startPos = new Vector3(0, 0, 0);

        /// <summary>
        /// The delay before the animation starts
        /// </summary>
        [Header("Timings")]
        public float delayBeforeStart = 0.5f;
        /// <summary>
        /// The delay before the button is unlocked after finishing the animation
        /// </summary>
        public float delayBeforeUnlock = 1.5f;

        [Header("Animation Amounts")]
        [Tooltip("The speed the button moves down in the first part of the enter animation")]
        public float moveDownSpeed = 1.0f;

        [Tooltip("The speed the button moves left in the second part of the enter animation")]
        public float moveLeftSpeed = 1.0f;

        [Tooltip("The speed the button moves left in the exit animation")]
        public float moveLeftOnHideSpeed = 1.0f;

        [Tooltip("The speed the color of the button changes in the enter animation")]
        public float colorChangeSpeed = 1.0f;

        [Tooltip("The amount the button moves down in the enter animation")]
        public float MoveDownAmount = 50.0f;

        [Tooltip("The amount the button moves left in the enter animation")]
        public float MoveLeftAmount = 100.0f;

        [Tooltip("The amount the button moves left in the exit animation")]
        public float MoveLeftOnHideAmount = 100.0f;

        [Tooltip("The time the button stalls before the pause menu begins to hide")]
        public float stallPauseMenuHide = 0.5f;

        [Tooltip("The speed the color fades out in the exit animation")]
        public float ColorFadeoutSpeed = 8;

        /// <summary>
        /// Whether or not the animator should use scaled time or unscaled time
        /// </summary>
        [Header("Settings")]
        public bool useUnscaledTime = false;

        [Header("Debug - DO NOT CHANGE"), SerializeField] private TextButton button;
        [SerializeField] private TMP_Text text;

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

        /// <summary>
        /// When the animation starts, this event is called. passed true if the animation is the entry animation, false if it is the exit animation.
        /// </summary>
        public event Action<bool> OnAnimationStart = delegate { };

        // Start is called before the first frame update
        void Start()
        {
            button = GetComponent<TextButton>();

            // The TextButton already requires a TMP_Text component, so we can safely assume it exists
            text = GetComponent<TMP_Text>();
            text.enabled = true;

            button.transform.localPosition = startPos;

            button.isDisabled = true;
            button.enabled = false;

            // set the color of the button to disabled with alpha 0
            Color color = button.disabledColor;
            color.a = 0;
            text.color = color;
            button.ChangeTargetColor(color);
        }

        private void OnEnable() => Start();

        /// <summary>
        /// Starts the animation of the button appearing.
        /// </summary>
        /// <returns></returns>
        public IEnumerator WaitToStartAnimation()
        {
            // set color of button to disabled with alpha 0
            Color color = button.disabledColor;
            color.a = 0;
            text.color = color;
            button.ChangeTargetColor(color);

            // set the color of the 
            yield return new WaitForSecondsRealtime(delayBeforeStart);
            OnAnimationStart(true);
            StartCoroutine(AnimationDown());
            StartCoroutine(AnimationColor());
        }

        public IEnumerator AnimationHide()
        { 
            if(!gameObject.activeSelf)
                yield break;

            OnAnimationStart(false);
            button.SuspendColorAnimation();
            StartCoroutine(AnimationFadeColorOut());
            Vector3 targetPos = new Vector3(startPos.x - MoveLeftOnHideAmount, startPos.y, 0);
            button.enabled = false;

            while (button.transform.localPosition.x > (targetPos.x + 0.04f))
            {
                // lerp the x position of the button to that of the start position
                float newX = Mathf.Lerp(button.transform.localPosition.x, targetPos.x, time * moveLeftOnHideSpeed);

                // set the new position
                button.transform.localPosition = new Vector3(newX, button.transform.localPosition.y, button.transform.localPosition.z);

                yield return null;
            }

            {
                Vector3 pos = button.transform.localPosition;
                pos.x = targetPos.x;
                button.transform.localPosition = pos;
            }
        }
        private IEnumerator AnimationDown()
        {
            button.OnMouseExit();
            // add MoveDownAmount to the y localPosition of the button
            button.transform.localPosition = new Vector3(button.transform.localPosition.x, startPos.y + MoveDownAmount, 0);
            // add MoveLeftAmount to the x localPosition of the button
            button.transform.localPosition = new Vector3(startPos.x + MoveLeftAmount, button.transform.localPosition.y, 0);

            // animate the button down
            while (button.transform.localPosition.y > (startPos.y + 0.04f))
            {
                // lerp the y position of the button to that of the start position
                float newY = Mathf.Lerp(button.transform.localPosition.y, startPos.y, time * moveDownSpeed);

                // set the new position
                button.transform.localPosition = new Vector3(button.transform.localPosition.x, newY, button.transform.localPosition.z);

                yield return null;

                if(button.transform.localPosition.y < startPos.y + 0.8f)
                {
                    StartCoroutine(AnimationLeft());
                }
            }

            {
                Vector3 pos = button.transform.localPosition;
                pos.y = startPos.y;
                button.transform.localPosition = pos;
            }
        }
        private IEnumerator AnimationLeft()
        {
            // animate the button left
            while (button.transform.localPosition.x > (startPos.x + 0.04f))
            {
                // lerp the x position of the button to that of the start position
                float newX = Mathf.Lerp(button.transform.localPosition.x, startPos.x, time * moveLeftSpeed);

                // set the new position
                button.transform.localPosition = new Vector3(newX, button.transform.localPosition.y, button.transform.localPosition.z);

                yield return null;
            }

            {
                Vector3 pos = button.transform.localPosition;
                pos.x = startPos.x;
                button.transform.localPosition = pos;
            }

            yield return new WaitForSecondsRealtime(delayBeforeUnlock);

            button.isDisabled = false;
            button.enabled = true;
        }
        private IEnumerator AnimationColor()
        {
            // gradually change the alpha of the text to 1

            while (text.color.a < 0.9)
            {
                Color color = text.color;
                color = Color.Lerp(color, button.normalColor, time * colorChangeSpeed);
                text.color = color;
                button.ChangeTargetColor(color);
                yield return null;


            }

            button.ResumeColorAnimation();
        }

        private IEnumerator AnimationFadeColorOut()
        {
            while (text.color.a > 0.01f)
            {
                // lerp color to alpha 0
                Color color = text.color;
                color.a = Mathf.Lerp(color.a, 0, time * ColorFadeoutSpeed);
                text.color = color;
                button.ChangeTargetColor(color);
                yield return null;
            }

            {
                Color color = text.color;
                color.a = 0;
                text.color = color;
                button.ChangeTargetColor(color);
            }

            StartCoroutine(AllowButtonReset());
        }
        private IEnumerator AllowButtonReset()
        {
            button.enabled = true;
            button.isDisabled = true;

            yield return new WaitForSecondsRealtime(button.hoverAnimationSpeed / time);
            button.enabled = false;
        }
    }
}