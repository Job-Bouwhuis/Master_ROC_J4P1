using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.WorldInteraction.InteractableItems
{
    public class HealthPack : MonoBehaviour, IWorldInteractable
    {
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
            
        }

        /// <summary>
        /// Function that gets called by the WorldInteractor system when the player looks at the item and presses the F key
        /// </summary>
        /// <param name="interactor">The WorldInteractor component that called this function</param>
        public void Interact(WorldInteractor interactor)
        {
            // Update Ammo count
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