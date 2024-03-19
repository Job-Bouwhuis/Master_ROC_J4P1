//Created by Niels
using ShadowUprising.UnityUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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

    public List<AudioContainer> audioContainers = new();
    private AudioSource audioSource;
    public event Action<AudioEventArgs> OnPlayerSoundPlayed = delegate { };

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioType audioType, Vector3 position) 
    {
        AudioContainer? container = audioContainers.Where(x => x.audioType == audioType).FirstOrDefault();
        if (container == null)
            return;
        audioSource.clip = container.audioClip;
        audioSource.Play();
        OnPlayerSoundPlayed(new(container, position));
    }
}
