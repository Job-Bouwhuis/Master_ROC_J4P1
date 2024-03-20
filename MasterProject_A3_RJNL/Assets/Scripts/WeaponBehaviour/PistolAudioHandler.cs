// Created by Niels
using ShadowUprising.Items.ItemFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAudioHandler : MonoBehaviour
{
    public AudioClip weaponShot;
    public AudioClip weaponReload;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        var comp = GetComponent<Pistol>();
        comp.onPistolReload += OnPistolReload;
        comp.onPistolShot += OnPistolShot;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnPistolShot()
    {
        audioSource.PlayOneShot(weaponShot);
    }

    private void OnPistolReload()
    {
        audioSource.PlayOneShot(weaponReload);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
