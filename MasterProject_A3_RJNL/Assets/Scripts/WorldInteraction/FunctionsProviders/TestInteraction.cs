using ShadowUprising.Inventory;
using ShadowUprising.Items;
using ShadowUprising.WorldInteraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.DEBUG
{
    public class TestInteraction : MonoBehaviour, IWorldInteractable
    {
        public Item item;

        public void Interact(WorldInteractor interactor)
        {
            InventoryManager.Instance.AddItem(item);
        }
    }
}