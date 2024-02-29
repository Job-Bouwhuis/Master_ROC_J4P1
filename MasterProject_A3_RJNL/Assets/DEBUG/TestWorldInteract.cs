using ShadowUprising.Inventory;
using ShadowUprising.Items;
using ShadowUprising.WorldInteraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWorldInteract : MonoBehaviour, IWorldInteractable
{
    public Item item;

    public int Priority => 0;

    public void Interact(WorldInteractor interactor)
    {
        InventoryManager.Instance.AddItem(item);
    }
}
