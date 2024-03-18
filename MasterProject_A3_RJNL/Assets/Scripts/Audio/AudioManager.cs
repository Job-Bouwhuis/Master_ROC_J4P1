//Created by Niels
using ShadowUprising.UnityUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace ShadowUprising.Audio
{

#nullable enable
    /// <summary>
    /// Audio manager for playing audio clips
    /// </summary>
    [DontDestroyOnLoad]
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        /// <summary>
        /// struct for audio events
        /// </summary>
        public readonly struct AudioEventArgs
        {
            /// <summary>
            /// The audio container that was played
            /// </summary>
            public readonly AudioContainer Container { get; }
            /// <summary>
            /// The position the audio was played at
            /// </summary>
            public readonly Vector3 Position { get; }

            /// <summary>
            /// Create a new audio event
            /// </summary>
            /// <param name="container"></param>
            /// <param name="position"></param>
            public AudioEventArgs(AudioContainer container, Vector3 position)
            {
                Container = container;
                Position = position;
            }
        }

        private AudioSource audioSource;
        /// <summary>
        /// Audio event for when a sound is played
        /// </summary>
        public event Action<AudioEventArgs> OnPlayerSoundPlayed = delegate { };

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// play audio at a position
        /// </summary>
        /// <param name="audio"></param>
        /// <param name="position"></param>
        public void Play(AudioContainer audio, Vector3 position)
        {
            var clip = audio.GetClip();
            if (audio == null)
                return;
            audioSource.clip = clip.clip;
            audioSource.pitch = clip.pitch;
            audioSource.Play();
            OnPlayerSoundPlayed(new(audio, position));
        }
    }
}