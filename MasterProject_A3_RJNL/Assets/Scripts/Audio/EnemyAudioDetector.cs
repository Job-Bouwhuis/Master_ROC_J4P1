// Created by Niels
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    /// <summary>
    /// Enemy audio detector is responsible to be the ears of the enemy
    /// </summary>
    public class EnemyAudioDetector : MonoBehaviour
    {
        /// <summary>
        /// hearing distance of the enemy
        /// </summary>
        public float hearDistance = 10f;
        private int threshold = 2;
        
        void Start()
        {
            AudioManager.Instance.OnPlayerSoundPlayed += OnPlayerSoundPlayed;
        }

        private void OnPlayerSoundPlayed(AudioManager.AudioEventArgs obj)
        {
            float distance = Vector3.Distance(obj.Position, transform.position);
            if (distance < hearDistance && (int)obj.Container.audioType >= threshold)
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