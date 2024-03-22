using ShadowUprising.UnityUtils;
using System;
using System.Collections.Generic;

namespace ShadowUprising.UI.InfoDialogs
{
    public class InfoDialogManager : Singleton<InfoDialogManager>
    {
        Queue<InfoDialogData> _infoDialogQueue = new();

        public void ShowInfoDialog(string title, string text, float duration)
        {
            ShowInfoDialog(new InfoDialogData(title, text, duration));
        }

        public void ShowInfoDialog(InfoDialogData infoDialogData)
        {
            _infoDialogQueue.Enqueue(infoDialogData);
            if (_infoDialogQueue.Count == 1)
            {
                ShowNextInfoDialog();
            }
        }

        private void ShowNextInfoDialog()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A struct that holds the data for an info dialog displayed by the <see cref="InfoDialogManager"/>
    /// </summary>
    public struct InfoDialogData
    {
        public string Title { get; }
        public string Text { get; }
        public float Duration { get; }

        public InfoDialogData(string title, string text, float duration)
        {
            Title = title;
            Text = text;
            Duration = duration;
        }
    }
}