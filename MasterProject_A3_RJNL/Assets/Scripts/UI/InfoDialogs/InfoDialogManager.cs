using ShadowUprising.UnityUtils;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace ShadowUprising.UI.InfoDialogs
{
    /// <summary>
    /// Class that manages the display of info dialogs in the game
    /// </summary>
    public class InfoDialogManager : Singleton<InfoDialogManager>
    {
        Queue<InfoDialogData> _infoDialogQueue = new();

        [SerializeField] private TextBackgroundAnimator TitleBackground;
        [SerializeField] private TextBackgroundAnimator TextBackground;
        [SerializeField] private SimpleTextAnimator TitleText;
        [SerializeField] private SimpleTextAnimator BodyText;

        private void Start()
        {
            StartCoroutine(ShowQueuedDialogs());
        }

        /// <summary>
        /// Shows an info dialog with the given title, text, and duration.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="duration"></param>
        public void ShowInfoDialog(string title, string text, float duration)
        {
            ShowInfoDialog(new InfoDialogData(title, text, duration));
        }

        /// <summary>
        /// Shows an info dialog with the given data.
        /// </summary>
        /// <param name="infoDialogData"></param>
        public void ShowInfoDialog(InfoDialogData infoDialogData)
        {
            _infoDialogQueue.Enqueue(infoDialogData);
        }

        private IEnumerator ShowQueuedDialogs()
        {
            while (true)
            {
                if(_infoDialogQueue.Count == 0)
                {
                    yield return null;
                    continue;
                }
                var dialog = _infoDialogQueue.Dequeue();

                TitleText.text = dialog.Title;
                BodyText.text = dialog.Text;
                TitleBackground.AnimateIn();
                TextBackground.AnimateIn();
                BodyText.StartWriting();
                TitleText.StartWriting();
                yield return new WaitForSeconds(dialog.Duration);
                TitleText.ClearText();
                BodyText.ClearText();
                TextBackground.AnimateOut();
                TitleBackground.AnimateOut();
                yield return new WaitForSeconds(2f);
            }
        }
    }

    /// <summary>
    /// A struct that holds the data for an info dialog displayed by the <see cref="InfoDialogManager"/>
    /// </summary>
    public readonly struct InfoDialogData
    {
        public readonly string Title { get; }
        public readonly string Text { get; }
        public readonly float Duration { get; }

        public InfoDialogData(string title, string text, float duration)
        {
            Title = title;
            Text = text;
            Duration = duration;
        }
    }
}