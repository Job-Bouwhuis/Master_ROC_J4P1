// Creator: Job
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShadowUprising.UI
{
    /// <summary>
    /// A button that has no background and only text. text colors, and animations can be set in inspector.
    /// <br></br> use the functions array to add functions to the button.
    /// <br></br> 
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI)), ExecuteAlways]
    public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Text"), Tooltip("The text  that appears as the button")]
        public string text;

        /// <summary>
        /// The normal color of the text
        /// </summary>
        [Header("Colors"), Tooltip("The normal color of the text")]
        public Color normalColor;
        /// <summary>
        /// THe color of the text when hovered
        /// </summary>
        [Tooltip("THe color of the button when hovered")]
        public Color hoverColor;
        /// <summary>
        /// The color of the button when pressed
        /// </summary>
        [Tooltip("The color of the button when pressed")]
        public Color pressedColor;
        /// <summary>
        /// The color of the button when disabled
        /// </summary>
        [Tooltip("The color of the button when disabled")]
        public Color disabledColor;

        [Header("Functions")]
        public ButtonFunction[] functions;

        /// <summary>
        /// If true, the button will not be interactable
        /// </summary>
        [Header("Settings")]
        [Tooltip("If true, makes the button act like a toggle")]
        public bool isToggle = false;
        [Tooltip("If true, the button will not be interactable")]
        public bool isDisabled = false;
        /// <summary>
        /// The speed at which the color fades to the target color
        /// </summary>
        [Tooltip("The speed at which the color fades to the target color")]
        public float colorFadeSpeed = 40;
        /// <summary>
        /// If true, the button will move to the right when hovered
        /// </summary>
        [Tooltip("If true, the button will move to the right when hovered")]
        public bool animaateOnHover = true;
        /// <summary>
        /// If true, the button will change scale when clicked
        /// </summary>
        [Tooltip("If true, the button will change scale when clicked")]
        public bool animateOnClick = true;
        /// <summary>
        /// The speed at which the button moves to the right when hovered
        /// </summary>
        [Tooltip("The speed at which the button moves to the right when hovered")]
        public float hoverAnimationSpeed = 20;
        /// <summary>
        /// The speed at which the button changes scale when clicked
        /// </summary>
        [Tooltip("The speed at which the button changes scale when clicked")]
        public float clickAnimationSpeed = 20;
        /// <summary>
        /// The amount the button moves to the right when hovered
        /// </summary>
        [Tooltip("The amount the button moves to the right when hovered")]
        public float hoverAnimationExtend = 50;
        /// <summary>
        /// The amount the button changes scale when clicked
        /// </summary>
        [Tooltip("The amount the button changes scale when clicked")]
        public float clickAnimationScaleIncrease = .2f;

        private bool isHovered = false;
        private bool isPressed = false;
        [Header("Debug - DO NOT EDIT")][SerializeField] private Color targetColor;

        private TMP_Text textComponent;
        private float startingWidth;
        private float animationTime = 0;
        [SerializeField] private bool toggleState = false;

        /// <summary>
        /// Sets hover to enabled
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovered = true;
        }
        /// <summary>
        /// Sets hover to disabled
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            isHovered = false;
        }

        private void Start()
        {
            textComponent = GetComponent<TMP_Text>();

            if (!Application.isPlaying)
            {
                return;
            }

            startingWidth = textComponent.rectTransform.sizeDelta.x;


            // fill functions with components of type ButtonFunction
            // dont add if the function already exists

            ButtonFunction[] temp = GetComponents<ButtonFunction>();
            foreach (ButtonFunction f in temp)
            {
                bool exists = false;

                Parallel.ForEach(functions, (f2, state) =>
                {
                    if (f == f2)
                    {
                        exists = true;
                        state.Break();
                    }
                });

                if (!exists)
                {
                    functions[functions.Length] = f;
                }
            }

            // set text
            textComponent.text = text;

            // set color to normal at start
            textComponent.color = normalColor;

        }
        private void Update()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && textComponent == null)
                textComponent = GetComponent<TMP_Text>();
#endif

            textComponent.text = text;

#if UNITY_EDITOR
            // if not in playmode, return
            if (!Application.isPlaying)
            {
                return;
            }
#endif

            UpdateState();

            UpdateColor();

            Animate();
        }
        private void Animate()
        {
            if (animaateOnHover)
            {
                // textcomponent text alignment to right
                textComponent.alignment = TextAlignmentOptions.Right;

                if (isHovered)
                {
                    animationTime = Mathf.Clamp(animationTime + Time.deltaTime * hoverAnimationSpeed, 0, 1);
                }
                else
                {
                    animationTime = Mathf.Clamp(animationTime - Time.deltaTime * hoverAnimationSpeed, 0, 1);
                }

                textComponent.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(startingWidth, startingWidth + hoverAnimationExtend, animationTime), textComponent.rectTransform.sizeDelta.y);
            }

            // lerp scale of button if pressed
            if (animateOnClick)
            {
                if (isPressed)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (1 + clickAnimationScaleIncrease), Time.deltaTime * clickAnimationSpeed);
                }
                else
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * clickAnimationSpeed);
                }
            }

        }
        private void UpdateState()
        {
            if (isDisabled)
            {
                isHovered = false;
                isPressed = false;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (isHovered)
                {
                    isPressed = true;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (isPressed)
                {
                    if (isHovered)
                    {
                        if (isToggle)
                        {
                            toggleState = !toggleState;
                        }
                        InvokeFunctions();
                    }
                }

                isPressed = false;
            }
            else if (Input.GetMouseButton(0))
            {
                if (!isHovered)
                {
                    isPressed = false;
                }
            }
            else
            {
                isPressed = false;
            }
        }
        private void InvokeFunctions()
        {
            foreach (ButtonFunction f in functions)
            {
                f.Invoke(this, isToggle);
            }
        }
        private void UpdateColor()
        {
            if (isDisabled)
                targetColor = disabledColor;
            else
            {
                if (isPressed)
                    targetColor = pressedColor;
                else if (isHovered)
                    targetColor = hoverColor;
                else if (isToggle && toggleState)
                    targetColor = pressedColor;
                else
                    targetColor = normalColor;
            }

            textComponent.color = LerpColor(textComponent.color, targetColor, colorFadeSpeed * Time.deltaTime);
        }
        private Color LerpColor(Color current, Color targetColor, float speed)
        {
            return Color.Lerp(current, targetColor, speed);
        }
    }
}