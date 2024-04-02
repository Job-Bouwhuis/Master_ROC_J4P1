using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    public class AIShooting : MonoBehaviour
    {
        /// <summary>
        /// time inbetween shots
        /// </summary>
        public float shootDelay;

        /// <summary>
        /// is called when the ai shoots the player
        /// </summary>
        public Action onAIShoot = delegate { };

        bool shooting;
        GuardState state;

        // Start is called before the first frame update
        void Start()
        {
            var comp = GetComponent<PlayerShootableDetector>();
            comp.onPlayerShootable += PlayerShootable;
            comp.onPlayerNotSchootable += PlayerNotShootable;
            state = GetComponentInParent<GuardState>();
            state.onStateChanged += AIStateChanged;
        }

        void AIStateChanged(AIState state)
        {
            if (!shooting && state == AIState.Attacking)
                StartCoroutine(ShootPlayer());
        }

        void PlayerShootable()
        {
            if (!shooting && state.CurrentState == AIState.Attacking)
                StartCoroutine(ShootPlayer());
        }

        void PlayerNotShootable()
        {
            shooting = false;
        }

        IEnumerator ShootPlayer()
        {
            shooting = true;
            while (shooting)
            {
                onAIShoot();
                yield return new WaitForSeconds(shootDelay);
            }
            
        }

    }
}
