using ShadowUprising.Player;
using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI.InfoDialogs
{
    /// <summary>
    /// Component that triggers an info dialog when the player enters a trigger collider, or when the component is enabled.
    /// </summary>
    public class InfoDialogTrigger : MonoBehaviour
    {
        [SerializeField] private string title;
        [SerializeField, Multiline(5)] private string message;
        [SerializeField] private float duration;

        [SerializeField] bool showOnEnable = false;
        [SerializeField] bool showOnTrigger = false;
        [SerializeField] bool showOnLoadingComplete = false;

        private void OnTriggerEnter(Collider other)
        {
            if (showOnTrigger && other.TryGetComponent<PlayerStats>(out _))
                InfoDialogManager.Instance.ShowInfoDialog(title, message, duration);
        }

        private void Start()
        {
            if (showOnEnable)
                InfoDialogManager.Instance.ShowInfoDialog(title, message, duration);

            if(showOnLoadingComplete && LoadingScreen.Instance != null)
                LoadingScreen.Instance.OnLoadingComplete += ShowDialog;
        }

        private void ShowDialog(int randomInt)
        {
            InfoDialogManager.Instance.ShowInfoDialog(title, message, duration);
        }
    }
}