using ShadowUprising.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    public class AIPlayerHealthInfluencer : MonoBehaviour
    {
        /// <summary>
        /// total damage dealt to the player
        /// </summary>
        public int damage;

        /// <summary>
        /// raycast range;
        /// </summary>
        public int range;

        Transform playerTransform;
        PlayerStats stats;


        // Start is called before the first frame update
        void Start()
        {
            GetComponent<AIShooting>().onAIShoot += RaycastToPlayer;
            playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
            stats = FindObjectOfType<PlayerStats>();
        }

        void RaycastToPlayer()
        {
            var heading = (playerTransform.position - transform.position).normalized;

            Physics.Raycast(new Ray(transform.position, heading), out RaycastHit hitinfo, range);
            if (hitinfo.transform != null)
                if (hitinfo.transform.tag == "Player")
                    stats.health -= damage;
                   // GetComponent<PlayerStats>().health -= damage;
            
        }
    }
}
