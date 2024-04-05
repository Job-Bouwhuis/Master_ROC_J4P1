// Creator: Ruben
// Editor: Job
using ShadowUprising.DeathSaves;
using ShadowUprising.Player;
using ShadowUprising.UI.Loading;
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
                DeathSaveManager.Instance.MakeSnapshot();
                DeathSaveManager.Instance.IsResetting = true;
                LoadingScreen.Instance.ShowAndLoad(sceneName);
            }
        }
    }
}