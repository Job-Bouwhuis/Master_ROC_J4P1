// Creator: Job

using ShadowUprising.UnityUtils;
using UnityEngine;
using System.Linq;
using WinterRose;

namespace ShadowUprising.WorldInteraction
{
    public class WorldInteractor : MonoBehaviour
    {
        public float interactDistance = 10f;

        public bool rayHit;
        Collider hitCollider;


        private void Start()
        {
        }

        private void Update()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance))
            {
                var interactables = hit.collider.gameObject.FindAllComponents<IWorldInteractable>();

                if (interactables.Any())
                {
                    rayHit = true;
                    hitCollider = hit.collider;


                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        interactables.Foreach(x => x.Interact(this));
                    }

                }
                else
                {
                    rayHit = false;
                }
            }
            else
            {
                rayHit = false;
            }
        }

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
    }
}