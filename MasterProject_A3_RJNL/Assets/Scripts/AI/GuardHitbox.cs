// Creator: Ruben
using System;
using ShadowUprising;
using ShadowUprising.WeaponBehaviour;
using UnityEngine;

namespace ShadowUprising.AI
{
    public class GuardHitbox : MonoBehaviour, IHitable
    {
        [SerializeField] int damageOnHit = 1;

        GuardHealth guardHealth;

        private void Start()
        {
            Assign();
        }

        void Assign()
        {
            guardHealth = GetComponentInParent<GuardHealth>();
            if (guardHealth == null)
                Log.PushError("A GuardHitbox is assigned to an object which parent does not include a GuardHealth component. This will make the GuardHitbox useless and cause many errors");
        }

        public void HitEvent()
        {
            guardHealth.RemoveHealth(damageOnHit);
        }
    }
}
