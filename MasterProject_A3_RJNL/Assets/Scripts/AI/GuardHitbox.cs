// Creator: Ruben
using System;
using ShadowUprising;
using ShadowUprising.WeaponBehaviour;
using UnityEngine;

namespace ShadowUprising.AI
{
    /// <summary>
    /// This component is for the hitbox the gun interacts with to damage the guard.
    /// Multiple hitboxes can be applied to a guard with different damage values to add weakpoints.
    /// This component should be applied to a child of the guard and should have an accompaning trigger collider.
    /// </summary>
    public class GuardHitbox : MonoBehaviour, IHitable
    {
        [Tooltip("Amount of damage to be applied to guard when this hitbox is hit")]
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
                Log.PushError("A GuardHitbox is assigned to an object which parent does not include a GuardHealth component. This will make the GuardHitbox useless and could cause many errors");
        }

        /// <summary>
        /// Function that gets called whenever this hitbox is hit by the players gun
        /// </summary>
        public void HitEvent()
        {
            guardHealth.RemoveHealth(damageOnHit);
        }
    }
}
