using ShadowUprising.Inventory;
using ShadowUprising.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.DEBUG
{
    /// <summary>
    /// Used for testing the giving of and interacting with items
    /// </summary>
    public class DEBUGGiveItemAndInteract : MonoBehaviour
    {
        /// <summary>
        /// The item that is given when the player pressed G
        /// </summary>
        [Header("press E to interact, press G to give and select")]
        public Item item;

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                GiveItem();
            }
        }

        void GiveItem()
        {
            InventoryManager.Instance.AddItem(item);
        }
    }
}