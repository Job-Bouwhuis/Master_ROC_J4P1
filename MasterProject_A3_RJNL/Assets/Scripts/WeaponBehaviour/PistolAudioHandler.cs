// Created by Niels
using ShadowUprising.Items.ItemFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    public class PistolAudioHandler : MonoBehaviour
    {
        public AudioContainer weaponShot;
        public AudioContainer weaponReload;
        // Start is called before the first frame update
        void Start()
        {
            var comp = GetComponent<Pistol>();
            comp.onPistolReload += OnPistolReload;
            comp.onPistolShot += OnPistolShot;
        }

        private void OnPistolShot()
        {
            AudioManager.Instance.Play(weaponShot, transform.position);
        }

        private void OnPistolReload()
        {
            AudioManager.Instance.Play(weaponReload, transform.position);
        }
    }
}