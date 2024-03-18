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

    [DontDestroyOnLoad]
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        public readonly struct AudioEventArgs
        {
            public readonly AudioContainer Container { get; }
            public readonly Vector3 Position { get; }

            public AudioEventArgs(AudioContainer container, Vector3 position)
            {
                Container = container;
                Position = position;
            }
        }

        private AudioSource audioSource;
        public event Action<AudioEventArgs> OnPlayerSoundPlayed = delegate { };

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

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