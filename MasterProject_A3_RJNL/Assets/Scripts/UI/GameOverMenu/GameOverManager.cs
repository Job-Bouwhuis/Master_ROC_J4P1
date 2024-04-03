using ShadowUprising.UI;
using ShadowUprising.UI.Loading;
using ShadowUprising.UI.PauseMenu;
using ShadowUprising.UnityUtils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ShadowUprising.GameOver
{
    /// <summary>
    /// A class that manages the handing of when the game is over.<br></br>
    /// Aswell as the UI that is displayed when the game is over.
    /// </summary>
    public class GameOverManager : Singleton<GameOverManager>
    {
        /// <summary>
        /// A flag to check if the game is over. once set to true, the only way to reset it is to reload the scene.<br></br><br></br>
        /// 
        /// In future, there will be a class specifically for handling the reloading of the game,
        /// where the last save will be initialized after the scene is reloaded.
        /// </summary>
        public bool IsGameOver { get; private set; }
        public event Action OnGameOver = delegate { };

        [Header("References")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private SimpleTextAnimator gameOverText;
        [SerializeField] private TextButtonAnimator restartButton;
        [SerializeField] private TextButtonAnimator mainMenuButton;

        [Header("Settings")]
        [SerializeField] private float backgroundFadeInSpeed = 1.0f;

        /// <summary>
        /// Makes the know that the game is over. <br></br>
        /// this will trigger the game over screen to appear and the <see cref="OnGameOver"/> event to be triggered<br></br>
        /// and lastly the <see cref="IsGameOver"/> flag to be set to true.
        /// </summary>
        public void GameOver()
        {
            if (IsGameOver)
                return;

            // this should be the only point where the game over flag is set.
            IsGameOver = true;
            StopAllCoroutines();
            backgroundImage.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            StartCoroutine(FadeInBackground());
            gameOverText.StartWriting();

            StartCoroutine(UnlockMouse());
            StartCoroutine(AnimateTimeScale());

            StartCoroutine(restartButton.WaitToStartAnimation());
            StartCoroutine(mainMenuButton.WaitToStartAnimation());

            OnGameOver();
        }

        /// <summary>
        /// Hides the game over screen, does not reset the <see cref="IsGameOver"/> flag.
        /// </summary>
        public void HideGameOver()
        {
            StopAllCoroutines();
            backgroundImage.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);
            StartCoroutine(restartButton.AnimationHide());
            StartCoroutine(mainMenuButton.AnimationHide());
        }

        protected override void Awake()
        {
            base.Awake();
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0);
            backgroundImage.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);

            if (LoadingScreen.Instance != null)
            {
                LoadingScreen.Instance.OnStartLoading.Subscribe(() =>
                {
                    HideGameOver();
                    Time.timeScale = 1;
                    return 0;
                });
            }
        }
        IEnumerator UnlockMouse()
        {
            yield return new WaitForSecondsRealtime(2.5f);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        IEnumerator AnimateTimeScale()
        {
            // lerp time scale to 0
            while (Time.timeScale > 0.01f)
            {
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.1f);
                yield return null;
            }

            Time.timeScale = 0;
        }
        IEnumerator FadeInBackground()
        {
            while (backgroundImage.color.a < 0.35f)
            {
                backgroundImage.color = new Color(
                    backgroundImage.color.r,
                    backgroundImage.color.g,
                    backgroundImage.color.b,
                    backgroundImage.color.a + (backgroundFadeInSpeed * Time.unscaledDeltaTime));
                yield return null;
            }

            backgroundImage.color = new Color(
                backgroundImage.color.r,
                backgroundImage.color.g,
                backgroundImage.color.b, 0.35f);
        }
    }
}