using ShadowUprising.WorldInteraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioTester : MonoBehaviour, IWorldInteractable
{
    [Header("Audio Settings"), Range(0.5f, 1.5f)]
    public float maxPitch = 1.5f;
    [Range(0.5f, 1.1f)]
    public float minPitch = 0.9f;

    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    public int Priority => 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
