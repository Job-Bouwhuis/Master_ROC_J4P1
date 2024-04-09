//Created by Niels
using ShadowUprising.UI.PauseMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    public class InGameAudio : MonoBehaviour
    {
        [SerializeField] AudioClip[] audioClips;
        private AudioSource audioSource;
        bool startedPlaying = false;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            SetAudio();
        }

        private void Update()
        {
            if (!PauseMenuManager.Instance.IsPaused)
            {
                if (!startedPlaying)
                {
                    startedPlaying = true;
                    PlayAudio();
                }
            }
            else
            {
                if (startedPlaying)
                {
                    startedPlaying = false;
                    PauseAudio();
                }
            }
        }

        void SetAudio()
        {
            if (audioClips.Length == 1)
            {
                audioSource.clip = audioClips[0];
                return;
            }
            else
            {
                audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            }
        }

        void PlayAudio()
        {
            audioSource.loop = true;
            audioSource.Play();
        }

        void PauseAudio()
        {
            audioSource.Pause();
        }
    }
}