// Creator: Ruben
using ShadowUprising.Player;
using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.LevelTriggers
{
    public class LoadingZone : MonoBehaviour
    {
        [SerializeField] string sceneName;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerStats>(out _))
            {
                LoadingScreen.Instance.ShowAndLoad(sceneName);
            }
        }
    }
}