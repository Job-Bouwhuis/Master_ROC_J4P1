using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    public class AIParticleHandler : MonoBehaviour
    {
        public ParticleSystem particles;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<AIShooting>().onAIShoot += OnAiShoot;
        }

        void OnAiShoot()
        {
            particles.Play();
        }
    }
}