using ShadowUprising.WorldInteraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioTester : MonoBehaviour, IWorldInteractable
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    public int Priority => 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact(WorldInteractor interactor)
    {
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
