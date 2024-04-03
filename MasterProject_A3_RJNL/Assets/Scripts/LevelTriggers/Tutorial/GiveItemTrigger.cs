using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowUprising;
using ShadowUprising.Player;
using ShadowUprising.Inventory;

namespace ShadowUprising.LevelTriggers.Tutorial
{
    /// <summary>
    /// This component is made to be put on a GameObject containing a trigger collider. 
    /// When the player goes through the trigger they will be given an item and this trigger will destroy itself.
    /// </summary>
    public class GiveItemTrigger : MonoBehaviour
    {
        [SerializeField] Items.Item item;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerStats>(out _))
            {
                if (InventoryManager.Instance != null)
                    InventoryManager.Instance.AddItem(item);
                Destroy(this);
            }
        }
    }
}