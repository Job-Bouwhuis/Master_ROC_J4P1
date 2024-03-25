// Created by Niels
using ShadowUprising.Items.ItemFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    /// <summary>
    /// Audio handler for the pistol
    /// </summary>
    public class PistolAudioHandler : MonoBehaviour
    {
        /// <summary>
        /// Audio container for the weapon shot
        /// </summary>
        public AudioContainer weaponShot;
        /// <summary>
        /// Audio container for the weapon reload
        /// </summary>
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