using ShadowUprising.UI.InGame;
using ShadowUprising.UnityUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.UI.PauseMenu
{
    public class PauseMenuManager : Singleton<PauseMenuManager>
    {
        [SerializeField] private ElementAnimator leftCover;
        [SerializeField] private ElementAnimator rightCover;

        [Tooltip("A list of UI elements to be called to show whenever the pause menu is activated")]
        [SerializeField] private List<ElementAnimator> UIElements = new();

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

        public void Pause()
        {
            OnPauseMenuShow();

            leftCover.ShowIndefinite();
            rightCover.ShowIndefinite();

            UIElements.Foreach(element => element.ShowIndefinite());

            // unlock mouse
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0;
        }

        public void Unpause()
        {
            StartCoroutine(StartHidingProcess());
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


            leftCover.HideFromIndefinite();
            rightCover.HideFromIndefinite();

            UIElements.Foreach(element => element.HideFromIndefinite());

            //lock mouse
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Time.timeScale = 1;

            yield return null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (leftCover.IsAtVisiblePosition)
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