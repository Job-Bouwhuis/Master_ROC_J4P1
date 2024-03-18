//Created by Niels
using ShadowUprising.WorldInteraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShadowUprising.Audio
{
    /// <summary>
    /// Audio tester for testing audio clips
    /// </summary>
    public class AudioTester : MonoBehaviour, IWorldInteractable
    {
        /// <summary>
        /// Maximum pitch for the audio
        /// </summary>
        [Header("Audio Settings"), Range(0.5f, 1.5f)]
        public float maxPitch = 1.5f;
        /// <summary>
        /// Minimum pitch for the audio
        /// </summary>
        [Range(0.5f, 1.1f)]
        public float minPitch = 0.9f;

        [SerializeField] private AudioClip[] audioClips;
        private AudioSource audioSource;

        /// <summary>
        /// priority of the interactable
        /// </summary>
        public int Priority => 0;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Interact with the audio tester
        /// </summary>
        /// <param name="interactor"></param>
        public void Interact(WorldInteractor interactor)
        {
            if (audioClips.Length == 0)
                return;

            audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);

            if (audioClips.Length == 1)
            {
                audioSource.clip = audioClips[0];
                audioSource.Play();
                return;
            }
            audioSource.clip = audioClips.OrderBy(clip => new System.Random().Next()).First();
            audioSource.Play();
        }
    }
}