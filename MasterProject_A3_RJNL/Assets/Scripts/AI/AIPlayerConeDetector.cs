//Creator: Luke
using System;
using UnityEngine;

namespace ShadowUprising.AI
{

    public class AIPlayerConeDetector : MonoBehaviour
    {

        /// <summary>
        /// the max range the dectector is able to spot the player from
        /// </summary>
        public int range;

        /// <summary>
        /// the max angle the detector is able to view
        /// </summary>
        public int coneAngle;

        /// <summary>
        /// is called when the player is detected
        /// </summary>
        public Action<Vector3> onPlayerDetected = delegate { };
        Transform playerTransform;

        private void Start()
        {
            Asign();
        }

        void Asign()
        {
            playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (DetectPlayer())
                OnPlayerDetected(playerTransform.position);
        }

        bool DetectPlayer()
        {
            var heading = (playerTransform.position - transform.position).normalized;
            if (Vector3.Angle(heading, transform.forward) < coneAngle)
            {
                Physics.Raycast(new Ray(transform.position, heading), out RaycastHit hitinfo, range);
                if (hitinfo.transform != null)
                    if (hitinfo.transform.tag == "Player")
                        return true;
            }
            return false;
        }

        void OnPlayerDetected(Vector3 playerPos)
        {
            onPlayerDetected.Invoke(playerPos);
        }

    }
}