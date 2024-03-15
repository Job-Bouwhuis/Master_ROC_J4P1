// Creator: Ruben
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    public class GuardHealth : MonoBehaviour
    {
        public int health;
        public GameObject deadGuard;

        private void Update()
        {
            CheckDeath();
        }

        void CheckDeath()
        {
            if (health <= 0)
            {
                Instantiate(deadGuard, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

        public void AddHealth(int addedHealth)
        {
            health += addedHealth;
        }

        public void RemoveHealth(int removedHealth)
        {
            health -= removedHealth;
        }
    }
}