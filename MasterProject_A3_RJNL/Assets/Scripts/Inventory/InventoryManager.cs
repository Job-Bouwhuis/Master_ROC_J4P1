// Creator: job

using ShadowUprising.Items;
using ShadowUprising.UnityUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using static ShadowUprising.Inventory.InventoryInteractionResult;

#nullable enable

namespace ShadowUprising.Inventory
{
    [DontDestroyOnLoad]
    public class InventoryManager : Singleton<InventoryManager>
    {
        public int maxUniqueItems = 6;

        public struct InventoryInteractResult
        {
            public InventoryInteractionResult Success { get; }
            public string Message { get; }

            public InventoryInteractResult(InventoryInteractionResult success, string message)
            {
                Success = success;
                Message = message;
            }
        }

        /// <summary>
        /// All items that can be found in the game<br></br><br></br>
        /// 
        /// <b>This list automatically updates when an item is added to the inventory. <br></br>
        /// when a given item is not yet encountered using the inventory system before, the item will be added and the item index of the item will be set automaitcally</b>
        /// </summary>
        public List<Item> allItems = new();

        /// <summary>
        /// The players inventory
        /// </summary>
        public List<Item> playerInventory = new();

        /// <summary>
        /// The amount of unique items in the inventory
        /// </summary>
        public int Count => playerInventory.Count;

        /// <summary>
        /// The item that is currently selected in the inventory.
        /// May be null if no item is selected
        /// </summary>
        public Item? SelectedItem { get; private set; }

        /// <summary>
        /// Returns false. The inventory is not read only
        /// </summary>
        public bool IsReadOnly => false;

        private void ValidateItem(Item item)
        {
            if (item.id == -1)
            {
                // check if there is already an item with the same name
                Item existing = allItems.Where(x => x.itemName == item.itemName).FirstOrDefault();
                if (existing != null)
                {
                    item.id = allItems.Find(x => x.itemName == item.itemName).id;
                }
                else
                {
                    item.id = allItems.Count;
                    allItems.Add(item);
                }
            }
        }

        /// <summary>
        /// Attempts to add an item to the inventory
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns></returns>
        public InventoryInteractResult AddItem(Item item)
        {
            ValidateItem(item);

            foreach(Item i in playerInventory)
            {
                if(i.id == item.id)
                {
                    if(i.CurrentStackSize < i.MaxStackSize)
                    {
                        i.SetStack(i.CurrentStackSize + item.CurrentStackSize);
                        return new InventoryInteractResult(Success | ItemAddedToStack, "Item added to stack");
                    }
                    else
                    {
                        return new InventoryInteractResult(Failure | StackFull, "Item stack is full");
                    }
                }
                playerInventory.Add(item);
                return new InventoryInteractResult(Success | ItemAdded, "Item added to inventory");
            }

            if(playerInventory.Count >= maxUniqueItems)
            {
                return new InventoryInteractResult(Failure | InventoryFull, "Inventory is full");
            }

            playerInventory.Add(item);
            return new InventoryInteractResult(Success | ItemAdded, "Item added to inventory");
        }

        public InventoryInteractResult RemoveItem(Item item)
        {
            ValidateItem(item);

            if(playerInventory.Contains(item))
            {
                playerInventory.Remove(item);
                return new InventoryInteractResult(Success | ItemRemoved, "Item removed from inventory");
            }
            else
            {
                return new InventoryInteractResult(Failure | ItemNotInInventory, "Item not in inventory");
            }
        }

        public InventoryInteractResult RemoveItem(Item item, int count)
        {
            ValidateItem(item);

            if(playerInventory.Contains(item))
            {
                if(item.CurrentStackSize > count)
                {
                    item.SetStack(item.CurrentStackSize - count);
                    return new InventoryInteractResult(Success | ItemRemovedFromStack, "Item removed from stack");
                }
                else
                {
                    playerInventory.Remove(item);
                    return new InventoryInteractResult(Success | ItemRemoved, "Item removed from inventory");
                }
            }
            else
            {
                return new InventoryInteractResult(Failure | ItemNotInInventory, "Item not in inventory");
            }
        }


        /// <summary>
        /// Selects the item at the given index in the inventory
        /// </summary>
        /// <param name="index">The index of the item that should be equipped</param>
        public InventoryInteractResult SelectIndex(int index)
        {
            if(index < 0 || index >= playerInventory.Count)
            {
                return new InventoryInteractResult(Failure | OutOfInventoryRange, "Index out of bounds");
            }

            SelectedItem = playerInventory[index];
            return new InventoryInteractResult(Success | ItemEquipped, "Item selected");
        }

        public InventoryInteractResult Interact()
        {
            if(SelectedItem == null)
            {
                return new InventoryInteractResult(Failure | NoItemSelected, "No item selected");
            }

            if(SelectedItem.ItemFunction == null)
            {
                return new InventoryInteractResult(Failure | ItemNotInteractable, "Item is not interactable");
            }

            SelectedItem.ItemFunction.UseItem();
            return new InventoryInteractResult(Success | ItemUsed, "Item interacted with");
        }
    }
}