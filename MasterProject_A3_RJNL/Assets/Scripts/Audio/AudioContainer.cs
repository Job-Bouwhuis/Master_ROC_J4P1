//Created by Niels
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.Audio
{
    /// <summary>
    /// Audio container for audio clips
    /// </summary>
    [CreateAssetMenu(fileName = "new Audio Container", menuName = "Audio/Audio Container")]
    public class AudioContainer : ScriptableObject
    {
        /// <summary>
        /// Audio clips to play, at least one is required
        /// </summary>
        public AudioClip[] audioClips;
        /// <summary>
        /// The type of audio this container is for
        /// </summary>
        public AudioType audioType;
        /// <summary>
        /// minimum pitch for the audio
        /// </summary>
        [Range(0.9f, 1.1f)]
        public float minPitch = 1f;
        /// <summary>
        /// maximum pitch for the audio
        /// </summary>
        [Range(0.9f, 1.1f)]
        public float maxPitch = 1f;

        /// <summary>
        /// Get a random clip from the audioClips array, if there is only one get that audio clip
        /// </summary>
        /// <returns></returns>
        public (AudioClip clip, float pitch) GetClip() => (audioClips[new System.Random().Next(0, audioClips.Length)], new System.Random().NextFloat(minPitch, maxPitch));
    }
}