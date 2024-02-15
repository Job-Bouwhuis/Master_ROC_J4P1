// Creator: job
using System.Collections;
using TMPro;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.UI.MainMenu
{
    public class MainMenuTextAnimator : MonoBehaviour
    {
        public float animationSpeed = 1;

        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextButton startButton;
        [SerializeField] private TextButton optionsButton;
        [SerializeField] private TextButton creditsButton;
        [SerializeField] private TextButton quitButton;

        const string TITLE = "Shadow Uprising";
        const string START = "Play Game";
        const string OPTIONS = "Options";
        const string CREDITS = "Credits";
        const string QUIT = "Quit";

        [SerializeField] private Vector3 titleStartPos;
        [SerializeField] private Vector3 StartGameStartPos;
        [SerializeField] private Vector3 optionsStartPos;
        [SerializeField] private Vector3 creditsStartPos;
        [SerializeField] private Vector3 quitStartPos;


        private RectTransform titleRectTransform;
        private RectTransform startButtonRectTransform;
        private RectTransform optionsButtonRectTransform;
        private RectTransform creditsButtonRectTransform;
        private RectTransform quitButtonRectTransform;

        private TextMeshProUGUI startButtonText;
        private TextMeshProUGUI optionsButtonText;
        private TextMeshProUGUI creditsButtonText;
        private TextMeshProUGUI quitButtonText;

        private Color titleDesiredColor;

        private float time;

        private void Awake()
        {
            titleRectTransform = title.rectTransform;
            startButtonRectTransform = startButton.GetComponent<RectTransform>();
            optionsButtonRectTransform = optionsButton.GetComponent<RectTransform>();
            creditsButtonRectTransform = creditsButton.GetComponent<RectTransform>();
            quitButtonRectTransform = quitButton.GetComponent<RectTransform>();

            startButtonText = startButton.GetComponent<TextMeshProUGUI>();
            optionsButtonText = optionsButton.GetComponent<TextMeshProUGUI>();
            creditsButtonText = creditsButton.GetComponent<TextMeshProUGUI>();
            quitButtonText = quitButton.GetComponent<TextMeshProUGUI>();

            title.text = "";
            startButton.text = "";
            optionsButton.text = "";
            creditsButton.text = "";
            quitButton.text = "";

            title.text = "";
            startButtonText.text = "";
            optionsButtonText.text = "";
            creditsButtonText.text = "";
            quitButtonText.text = "";

            titleStartPos = title.rectTransform.anchoredPosition;
            StartGameStartPos = startButtonRectTransform.anchoredPosition;
            optionsStartPos = optionsButtonRectTransform.anchoredPosition;
            creditsStartPos = creditsButtonRectTransform.anchoredPosition;
            quitStartPos = quitButtonRectTransform.anchoredPosition;

            int screenWidth = Screen.width + 300;

            // set positions of buttons and title to be off screen on the right
            title.rectTransform.anchoredPosition = new Vector2(screenWidth, 0);
            startButtonRectTransform.anchoredPosition = new Vector2(screenWidth, 0);
            optionsButtonRectTransform.anchoredPosition = new Vector2(screenWidth, 0);
            creditsButtonRectTransform.anchoredPosition = new Vector2(screenWidth, 0);
            quitButtonRectTransform.anchoredPosition = new Vector2(screenWidth, 0);

            titleDesiredColor = title.color;

            title.color = new Color(0, 0, 0, 0);
            startButtonText.color = new Color(0, 0, 0, 0);
            optionsButtonText.color = new Color(0, 0, 0, 0);
            creditsButtonText.color = new Color(0, 0, 0, 0);
            quitButtonText.color = new Color(0, 0, 0, 0);

            startButton.enabled = false;
            optionsButton.enabled = false;
            creditsButton.enabled = false;
            quitButton.enabled = false;
        }

        void Start()
        {
            StartCoroutine(AnimateText());
        }

        private void Update()
        {
            // lerp title and buttons to their start positions
            title.rectTransform.anchoredPosition = Vector3.Lerp(title.rectTransform.anchoredPosition, titleStartPos, Time.deltaTime * animationSpeed);
            startButtonRectTransform.anchoredPosition = Vector3.Lerp(startButtonRectTransform.anchoredPosition, StartGameStartPos, Time.deltaTime * animationSpeed);
            optionsButtonRectTransform.anchoredPosition = Vector3.Lerp(optionsButtonRectTransform.anchoredPosition, optionsStartPos, Time.deltaTime * animationSpeed);
            creditsButtonRectTransform.anchoredPosition = Vector3.Lerp(creditsButtonRectTransform.anchoredPosition, creditsStartPos, Time.deltaTime * animationSpeed);
            quitButtonRectTransform.anchoredPosition = Vector3.Lerp(quitButtonRectTransform.anchoredPosition, quitStartPos, Time.deltaTime * animationSpeed);

            // lerp the color of the buttons from transparent to opaque
            title.color = Color.Lerp(title.color, titleDesiredColor, Time.deltaTime * (animationSpeed / 2));
            startButtonText.color = Color.Lerp(startButtonText.color, new Color(startButtonText.color.r, startButtonText.color.g, startButtonText.color.b, 1), Time.deltaTime * (animationSpeed / 2));
            optionsButtonText.color = Color.Lerp(optionsButtonText.color, new Color(optionsButtonText.color.r, optionsButtonText.color.g, optionsButtonText.color.b, 1), Time.deltaTime * (animationSpeed / 2));
            creditsButtonText.color = Color.Lerp(creditsButtonText.color, new Color(creditsButtonText.color.r, creditsButtonText.color.g, creditsButtonText.color.b, 1), Time.deltaTime * (animationSpeed / 2));
            quitButtonText.color = Color.Lerp(quitButtonText.color, new Color(quitButtonText.color.r, quitButtonText.color.g, quitButtonText.color.b, 1), Time.deltaTime * (animationSpeed / 2));


            time += Time.deltaTime * animationSpeed;

            if (time >= animationSpeed - Time.deltaTime * animationSpeed)
            {
                startButton.enabled = true;
                optionsButton.enabled = true;
                creditsButton.enabled = true;
                quitButton.enabled = true;
            }
        }


        private IEnumerator AnimateText()
        {
            StartCoroutine(AnimateTitle());
            yield return new WaitForSeconds(.3f);

            StartCoroutine(AnimateButton(startButton, START));
            yield return new WaitForSeconds(.2f);
            StartCoroutine(AnimateButton(optionsButton, OPTIONS));
            yield return new WaitForSeconds(.2f);
            StartCoroutine(AnimateButton(creditsButton, CREDITS));
            yield return new WaitForSeconds(.2f);
            StartCoroutine(AnimateButton(quitButton, QUIT));
        }

        private IEnumerator AnimateButton(TextButton button, string text)
        {
            foreach (int i in text.Length)
            {
                yield return new WaitForSeconds(0.1f);
                button.text = text[..i];
            }
            button.text = text;
        }

        IEnumerator AnimateTitle()
        {
            foreach (int i in TITLE.Length)
            {
                yield return new WaitForSeconds(0.1f);
                title.text = TITLE[..i];
            }
            title.text = TITLE;
        }
    }
}