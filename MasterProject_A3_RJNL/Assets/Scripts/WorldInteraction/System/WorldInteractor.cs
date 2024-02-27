// Creator: Job

using ShadowUprising.UnityUtils;
using UnityEngine;
using System.Linq;
using WinterRose;

namespace ShadowUprising.WorldInteraction
{
    /// <summary>
    /// A unity script that allows the player to interact with specific objects in the world.
    /// </summary>
    public class WorldInteractor : MonoBehaviour
    {
        public float interactDistance = 10f;

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

                if (interactables.Any())
                {
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
#if UNITY_EDITOR
                else
                    rayHit = false;
#endif
            }
#if UNITY_EDITOR
            else
                rayHit = false;
#endif
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