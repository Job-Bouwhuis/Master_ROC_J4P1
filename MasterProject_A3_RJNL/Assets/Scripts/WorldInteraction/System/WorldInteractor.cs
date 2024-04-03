// Creator: Job

using ShadowUprising.UnityUtils;
using UnityEngine;
using System.Linq;
using WinterRose;
using System;

namespace ShadowUprising.WorldInteraction
{
    /// <summary>
    /// A unity script that allows the player to interact with specific objects in the world.
    /// </summary>
    public class WorldInteractor : MonoBehaviour
    {
        [Tooltip("The distance the player can interact with objects.")]
        public float interactDistance = 10f;

        public Action OnItemLookedAt = delegate { };
        public Action OnItemLookedAtStopped = delegate { };
        private bool stoppedLookingAtItem = false;
#if UNITY_EDITOR
        [Header("Debug")]
        public bool rayHit;
#endif
        Collider hitCollider;

        private void Update()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance))
            {
                var interactables = hit.collider.gameObject.FindAllComponents<IWorldInteractable>();
                interactables = interactables.OrderBy(x => x.Priority);

                if (interactables.Any())
                {
                    OnItemLookedAt();
                    stoppedLookingAtItem = false;
                    hitCollider = hit.collider;
#if UNITY_EDITOR
                    rayHit = true;
#endif
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        interactables.Foreach(x => x.Interact(this));
                        Log.Push("Interacted with world object");
                    }

                }
                else
                {
#if UNITY_EDITOR
                    rayHit = false;
#endif
                    if (!stoppedLookingAtItem)
                    {
                        OnItemLookedAtStopped();
                        stoppedLookingAtItem = true;
                    }
                }
            }
            else
            {
#if UNITY_EDITOR
                rayHit = false;
#endif
                if (!stoppedLookingAtItem)
                {
                    OnItemLookedAtStopped();
                    stoppedLookingAtItem = true;
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * interactDistance);


            if (rayHit)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(hitCollider.bounds.center, hitCollider.bounds.size);
            }
        }
#endif
    }
}