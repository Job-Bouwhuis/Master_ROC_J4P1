//Created by Niels
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.Audio
{
    [CreateAssetMenu(fileName = "new Audio Container", menuName = "Audio/Audio Container")]
    public class AudioContainer : ScriptableObject
    {
        public AudioClip[] audioClips;
        public AudioType audioType;
        [Range(0.9f, 1.1f)]
        public float minPitch = 1f;
        [Range(0.9f, 1.1f)]
        public float maxPitch = 1f;

        public (AudioClip clip, float pitch) GetClip() => (audioClips[new System.Random().Next(0, audioClips.Length)], new System.Random().NextFloat(minPitch, maxPitch));
    }
}