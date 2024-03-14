// Creator: Ruben
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    public class GuardHealth : MonoBehaviour
    {
        public int health;

        private void Update()
        {
            CheckDeath();
        }

        void CheckDeath()
        {
            if (health <= 0)
                // TODO: add death event
                return;
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