//Created by Niels
using ShadowUprising.UI.PauseMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace ShadowUprising.Audio
{
    public class PauseMenuAudio : MonoBehaviour
    {
        [SerializeField] AudioClip pauseMenuMusic;
        AudioSource audioSource;
        bool startedPlaying = false;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void Update()
        {
            if (PauseMenuManager.Instance.IsPaused)
            {
                if (!startedPlaying)
                {
                    startedPlaying = true;
                    audioSource.clip = pauseMenuMusic;
                    audioSource.Play();
                }
            }
            else
            {
                if (startedPlaying)
                {
                    startedPlaying = false;
                    audioSource.Stop();
                }
            }
        }
    }
}