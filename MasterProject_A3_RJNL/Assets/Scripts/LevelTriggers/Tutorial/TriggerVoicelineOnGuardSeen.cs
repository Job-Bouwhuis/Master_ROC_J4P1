// Creator: Job
using ShadowUprising.Audio;
using ShadowUprising.Settings;
using ShadowUprising.UnityUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Tutorial
{
    /// <summary>
    /// Component that triggers a voiceline when the player sees a guard, and destroys itself when the voice line is played
    /// </summary>
    public class TriggerVoicelineOnGuardSeen : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] GameObject guarddetector;

        // Update is called once per frame
        void Update()
        {
            if (guarddetector.IsDestroyed()) return;
            if (!guarddetector.activeSelf) return;

            // Perform the linecast and check if it hit anything
            if (Physics.Linecast(player.transform.position, guarddetector.transform.position, out RaycastHit hit))
            {
                if (hit.collider.gameObject != guarddetector) return;
                VoiceTutorialManager.Instance.PlayNextVoiceLine();
                Destroy(guarddetector);
                Destroy(gameObject);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (guarddetector.IsDestroyed()) return;
            if (!guarddetector.activeSelf) return;

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(player.transform.position, guarddetector.transform.position);
        }
#endif
    }
}