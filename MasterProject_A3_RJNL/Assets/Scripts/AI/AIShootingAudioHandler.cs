using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShadowUprising.AI
{
    public class AIShootingAudioHandler : MonoBehaviour
    {
        /// <summary>
        /// contains the audio clip of the shooting sound
        /// </summary>
        public AudioClip shootingClip;
        AudioSource audioSource;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            GetComponent<AIShooting>().onAIShoot += onPlayerShoot;
        }

        void onPlayerShoot()
        {
            audioSource.PlayOneShot(shootingClip);
        }
    }
}
