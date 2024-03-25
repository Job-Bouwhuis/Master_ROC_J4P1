using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShadowUprising.AI
{
    public class PlayerShootableDetector : MonoBehaviour
    {

        public float angle;

        public Action onPlayerShootable = delegate { };
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
            shootable = Vector3.Angle(transform.forward, player.transform.position) < angle;
        }
    }
}