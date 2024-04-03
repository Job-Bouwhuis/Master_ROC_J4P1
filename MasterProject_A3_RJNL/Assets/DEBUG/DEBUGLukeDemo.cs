using ShadowUprising.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.DEBUG
{
    public class DEBUGLukeDemo : MonoBehaviour
    {
        private GameObject spawnedCoeb;
        /// <summary>
        /// the place where 'spawnedCoeb' will be placed
        /// </summary>
        public Transform spawnLocation;

        // Start is called before the first frame update
        void Start()
        {
            InventoryManager.Instance.OnInventoryInteract += lukedemo2;
        }


        private void lukedemo2(InventoryManager.InventoryInteractResult result)
        {
            if (result.Status.HasFlag(InventoryInteractionResult.Failure))
                return;

            if (result.Status.HasFlag(InventoryInteractionResult.ItemEquipped))
            {
                if (result.Item == null)
                {
                    if (spawnedCoeb != null)
                        Destroy(spawnedCoeb);
                    return;
                }

                if (result.Item.itemName == "Health Pack")
                {
                    spawnedCoeb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    spawnedCoeb.transform.position = spawnLocation.position;
                    spawnedCoeb.transform.parent = spawnLocation;
                    spawnedCoeb.transform.rotation = spawnLocation.rotation;
                }
                else
                    if (spawnedCoeb != null)
                    Destroy(spawnedCoeb);
            }
        }
    }
}