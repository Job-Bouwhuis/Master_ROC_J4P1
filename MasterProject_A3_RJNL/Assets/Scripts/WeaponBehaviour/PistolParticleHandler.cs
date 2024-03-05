using ShadowUprising.Items.ItemFunctions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShadowUprising.WeaponBehaviour
{
    public class PistolParticleHandler : MonoBehaviour
    {
        /// <summary>
        /// particles to be played
        /// </summary>
        public ParticleSystem particles;

        /// <summary>
        /// plays the partciles
        /// </summary>
        public void PlayParticles()
        {
            particles.Play();
        }

        private void Start()
        {
            GetComponent<Pistol>().onPistolShot += PlayParticles;
        }
    }
}
