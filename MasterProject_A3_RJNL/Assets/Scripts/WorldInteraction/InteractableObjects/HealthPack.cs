// Creator: Ruben
using System.Collections;
using System.Collections.Generic;
using ShadowUprising;
using ShadowUprising.Player;
using UnityEngine;

namespace ShadowUprising.WorldInteraction.InteractableItems
{
    public class HealthPack : MonoBehaviour, IWorldInteractable
    {
        PlayerStats playerStats;

        [Tooltip("The amount of health this item restores to the player when used")]
        [SerializeField] int healingAmount;

        /// <summary>
        /// The priority of the object for the player interaction
        /// </summary>
        public int Priority => 0;

        private void Start()
        {
            Assign();
        }

        void Assign()
        {
            playerStats = FindObjectOfType<PlayerStats>();
            if (playerStats == null)
                Log.PushWarning("There exists a HealthPack in this scene but no PlayerStats component on the player. Health cannot be updated if player does not have health");
        }

        /// <summary>
        /// Function that gets called by the WorldInteractor system when the player looks at the item and presses the F key
        /// </summary>
        /// <param name="interactor">The WorldInteractor component that called this function</param>
        public void Interact(WorldInteractor interactor)
        {
            playerStats.AddHealth(healingAmount);
            DestroySelf();
        }

        /// <summary>
        /// Function called that destroys the current object
        /// </summary>
        public void DestroySelf()
        {
            Destroy(this.gameObject);
        }
    }
}