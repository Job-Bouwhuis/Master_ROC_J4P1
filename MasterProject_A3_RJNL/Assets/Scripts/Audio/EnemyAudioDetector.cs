using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    public class EnemyAudioDetector : MonoBehaviour
    {
        public float hearDistance = 10f;
        void Start()
        {
            AudioManager.Instance.OnPlayerSoundPlayed += OnPlayerSoundPlayed;
        }

        private void OnPlayerSoundPlayed(AudioManager.AudioEventArgs obj)
        {
            // detemian distane between player and me
            float distance = Vector3.Distance(obj.Position, transform.position);
            // if distance is smaller than hearDistnace
            if (distance < hearDistance + (int)obj.Container.audioType)
            {
                // go to pos
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, hearDistance);
        }
#endif
    }
}