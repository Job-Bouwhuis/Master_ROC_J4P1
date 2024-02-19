using ShadowUprising.Inventory;
using ShadowUprising.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Debug
{
    /// <summary>
    /// Used for testing the giving of and interacting with items
    /// </summary>
    public class GiveItemAndInteract : MonoBehaviour
    {
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