// Creator: Job
using ShadowUprising.UI.Loading;
using ShadowUprising.UnityUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.UI.PauseMenu
{
    /// <summary>
    /// Manages the pause menu
    /// 
    /// <br></br><br></br>
    /// Singleton, not in DontDestroyOnLoad
    /// </summary>
    public class PauseMenuManager : Singleton<PauseMenuManager>
    {
        /// <summary>
        /// Called when the pause menu is set to show
        /// </summary>
        public Action OnPauseMenuShow = delegate { };
        /// <summary>
        /// Called when the pause menu is set to hide
        /// <br></br><br></br>
        /// All subscribers should return the time in seconds they request for the pause menu to wait before starting the hiding process.
        /// </summary>
        public MultipleReturnEvent<float> OnPauseMenuHide = new();

        [Header("Settings")]
        [Tooltip("The left cover of the pause menu")]
        [SerializeField] private ElementAnimator leftCover;

        [Tooltip("The right cover of the pause menu")]
        [SerializeField] private ElementAnimator rightCover;

        [Tooltip("A list of UI elements to be called to show whenever the pause menu is activated")]
        [SerializeField] private List<ElementAnimator> UIElements = new();

        [Tooltip("The root object for the options menu.")]
        [SerializeField] private GameObject optionsRoot;

        [Tooltip("The root for the buttons on the pause menu")]
        [SerializeField] private GameObject buttonsRoot;

        public bool IsPaused { get; private set; }

        public bool OptionsOpen { get; private set; }

        /// <summary>
        /// Pauses the game and shows the pause menu
        /// </summary>
        public void Pause()
        {
            OnPauseMenuShow();

            leftCover.ShowIndefinite();
            rightCover.ShowIndefinite();

            UIElements.Foreach(element => element.ShowIndefinite());

            StartCoroutine(ShowMouse());

            IsPaused = true;
            Time.timeScale = 0;
        }

        /// <summary>
        /// Unpauses the game and hides the pause menu
        /// </summary>
        public void Unpause()
        {
            IsPaused = false;
            StopAllCoroutines();
            StartCoroutine(StartHidingProcess());
        }

        IEnumerator ShowMouse()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Log.Push("Moving mouse Away");
            // unlock mouse
            Windows.SetMousePosition(0, 0);
            yield return new WaitForSecondsRealtime(0.2f);
            Log.Push("Moving mouse to center");
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Windows.SetMousePosition(screenCenter.x.FloorToInt(), screenCenter.y.FloorToInt());
        }

        public void OpenOptions()
        {
            OptionsOpen = true;
            UIElements.Foreach(UIElements => UIElements.HideImmediately());
            optionsRoot.SetActive(true);
            buttonsRoot.SetActive(false);

            foreach(Transform transform in optionsRoot.transform)
            {
                if (transform.TryGetComponent(out TextButton button))
                    button.OnPointerExit(null);
            }
        }

        public void CloseOptions()
        {
            OptionsOpen = false;
            UIElements.Foreach(UIElements => UIElements.ShowIndefinite());
            optionsRoot.SetActive(false);
            buttonsRoot.SetActive(true);

            foreach (Transform transform in buttonsRoot.transform)
            {
                if (transform.TryGetComponent(out TextButton button))
                    button.OnPointerExit(null);
            }
        }

        IEnumerator StartHidingProcess()
        {
            var times = OnPauseMenuHide.Invoke();

            Log.Push($"{times.Count} subscribers to the hiding of the pause menu");
            if (times.Count is 0)
                times.Add(0);
            var maxTime = times.Max();

            Log.Push($"Waiting {maxTime} seconds before hiding pause menu");

            if (maxTime > 0)
                yield return new WaitForSecondsRealtime(maxTime);

            leftCover.HideImmediately();
            rightCover.HideImmediately();

            UIElements.Foreach(element => element.HideFromIndefinite());

            //lock mouse
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            yield return null;
        }
        private void Start()
        {
            if(LoadingScreen.Instance != null)
            {
                LoadingScreen.Instance.OnStartLoading.Subscribe(() =>
                {
                    Unpause();
                    return 1f;
                });
            }

            optionsRoot.SetActive(false);
            buttonsRoot.SetActive(true);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (IsPaused)
                {
                    Unpause();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
}