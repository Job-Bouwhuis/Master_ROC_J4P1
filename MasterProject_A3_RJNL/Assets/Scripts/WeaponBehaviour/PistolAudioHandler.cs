// Created by Niels
//Edited by: Luke
using ShadowUprising.Items.ItemFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    public class PistolAudioHandler : MonoBehaviour
    {
        public AudioClip weaponShot;
        public AudioClip weaponReload;
        public AudioClip weaponShotEmpty;
        private AudioSource audioSource;
        // Start is called before the first frame update
        void Start()
        {
            var comp = GetComponent<Pistol>();
            comp.onPistolReload += OnPistolReload;
            comp.onPistolShot += OnPistolShot;
            comp.onPistolShootEmtpy += onPistolShotEmpty;
            audioSource = GetComponent<AudioSource>();
        }

        private void onPistolShotEmpty()
        {
            audioSource.PlayOneShot(weaponShotEmpty);
        }

        private void OnPistolShot()
        {
            audioSource.PlayOneShot(weaponShot);
        }

        private void OnPistolReload()
        {
            audioSource.PlayOneShot(weaponReload);
        }
    }

}
