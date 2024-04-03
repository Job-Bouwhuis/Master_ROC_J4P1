using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShadowUprising.AI
{
    public class PlayerShootableDetector : MonoBehaviour
    {
        /// <summary>
        /// determines the shooting angle
        /// </summary>
        public float angle;

        /// <summary>
        /// is called when the player is shootable
        /// </summary>
        public Action onPlayerShootable = delegate { };
        /// <summary>
        /// is called when the player isnt shootable anymore
        /// </summary>
        public Action onPlayerNotSchootable = delegate { };

        GameObject player;

        bool _shootable;
        bool shootable
        {
            get { return _shootable; }
            set
            {
                if (_shootable != value)
                {
                    CallEvents(value);
                }
                _shootable = value;
            }
        }

        private void CallEvents(bool value)
        {
            if (value)
                onPlayerShootable();
            else
                onPlayerNotSchootable();
        }

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            var heading = (player.transform.position - transform.position).normalized;
            shootable = Vector3.Angle(heading, transform.forward) < angle;
        }
    }
}