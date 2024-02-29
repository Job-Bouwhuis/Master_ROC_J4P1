using System.Collections;
using TMPro;
using UnityEngine;

namespace ShadowUprising.UI.PauseMenu
{
    [RequireComponent(typeof(TextButton))]
    public class TextButtonAnimator : MonoBehaviour
    {
        [Header("Timings")]
        public float delayBeforeStart = 0.5f;
        public float delayBeforeUnlock = 1.5f;

        [Header("Animation Amounts")]
        public float moveDownSpeed = 1.0f;
        public float moveLeftSpeed = 1.0f;
        public float moveLeftOnHideSpeed = 1.0f;
        public float colorChangeSpeed = 1.0f;
        public float MoveDownAmount = 50.0f;
        public float MoveLeftAmount = 100.0f;
        public float MoveLeftOnHideAmount = 100.0f;
        public float stallPauseMenuHide = 0.5f;

        [Header("Settings")]
        public bool useUnscaledTime = false;

        [Header("Debug - DO NOT CHANGE"), SerializeField] private TextButton button;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Vector3 startPos;

        /// <summary>
        /// A private shortcut to the time variable that returns the unscaled time if <see cref="useUnscaledTime"/> is true
        /// </summary>
        private float time => useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

        // Start is called before the first frame update
        void Start()
        {
            PauseMenuManager.Instance.OnPauseMenuShow += () =>
            {
                StopAllCoroutines();
                StartCoroutine(WaitToStartAnimation());
            };

            PauseMenuManager.Instance.OnPauseMenuHide.Subscribe(() =>
            {
                StopAllCoroutines();
                StartCoroutine(AnimationHide());
                return delayBeforeUnlock;
            });

            button = GetComponent<TextButton>();
            button = GetComponent<TextButton>();

            // The TextButton already requires a TMP_Text component, so we can safely assume it exists
            text = button.GetComponent<TMP_Text>();

            button.isDisabled = true;
            button.enabled = false;

            // set the color of the button to disabled with alpha 0
            Color color = button.disabledColor;
            color.a = 0;
            text.color = color;
            button.ChangeTargetColor(color);
        }

        IEnumerator WaitToStartAnimation()
        {
            button.transform.position = startPos;
            Log.Push("Animation started");

            // set color of button to disabled with alpha 0
            Color color = button.disabledColor;
            color.a = 0;
            text.color = color;
            button.ChangeTargetColor(color);

            // set the color of the 
            yield return new WaitForSecondsRealtime(delayBeforeStart);
            Log.Push("Delay Passed");
            StartCoroutine(AnimationDown());
            StartCoroutine(AnimationColor());
        }

        IEnumerator AnimationDown()
        {
            // add MoveDownAmount to the y localPosition of the button
            button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y + MoveDownAmount, 0);
            // add MoveLeftAmount to the x localPosition of the button
            button.transform.position = new Vector3(button.transform.position.x + MoveLeftAmount, button.transform.position.y, 0);

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

        IEnumerator AnimationLeft()
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

        IEnumerator AnimationColor()
        {
            // gradually change the alpha of the text to 1

            while (text.color.a < 1)
            {
                Color color = text.color;
                color = Color.Lerp(color, button.normalColor, time * colorChangeSpeed);
                text.color = color;
                button.ChangeTargetColor(color);
                yield return null;
            }

            {
                text.color = button.normalColor;
                button.ChangeTargetColor(button.normalColor);
            }
        }

        IEnumerator AnimationHide()
        {
            Vector3 targetPos = new Vector3(startPos.x - MoveLeftOnHideAmount, startPos.y, 0);
            button.enabled = false;

            while (button.transform.localPosition.x < (targetPos.x - 0.04f))
            {
                // lerp the x position of the button to that of the start position
                float newX = Mathf.Lerp(button.transform.localPosition.x, targetPos.x, time * moveLeftOnHideSpeed);

                // set the new position
                button.transform.localPosition = new Vector3(newX, button.transform.localPosition.y, button.transform.localPosition.z);

                // lerp color to alpha 0
                Color color = text.color;
                color.a = Mathf.Lerp(color.a, 0, time * colorChangeSpeed);
                text.color = color;
                button.ChangeTargetColor(color);

                yield return null;
            }

            {
                Vector3 pos = button.transform.localPosition;
                pos.x = targetPos.x;
                button.transform.localPosition = pos;
            }
        }
    }
}