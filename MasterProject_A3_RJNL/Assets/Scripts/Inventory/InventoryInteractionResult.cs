// Creator: job

namespace ShadowUprising.Inventory
{
    /// <summary>
    /// A result of an interaction with the inventory
    /// </summary>
    public enum InventoryInteractionResult
    {
        /// <summary>
        /// The interaction overall was successful. More information can be found by checking the individual flags
        /// </summary>
        Success = 0,
        /// <summary>
        /// The interaction overall was a failure. More information can be found by checking the individual flags
        /// </summary>
        Failure = 1,

        /// <summary>
        /// The item was added to the inventory
        /// </summary>
        ItemAdded = 2,
        /// <summary>
        /// The item was added to a stack in the inventory
        /// </summary>
        ItemAddedToStack = 4,
        /// <summary>
        /// The item was removed from the inventory
        /// </summary>
        ItemRemoved = 8,
        /// <summary>
        /// The item was removed from a stack in the inventory
        /// </summary>
        ItemRemovedFromStack = 16,
        /// <summary>
        /// The stack is full
        /// </summary>
        StackFull = 32,
        /// <summary>
        /// The item was not found as an item (in the inventory or at all)
        /// </summary>
        ItemNotFound = 64,
        /// <summary>
        /// The item is not stackable, user is already carrying one
        /// </summary>
        ItemNotStackable = 128,
        /// <summary>
        /// The inventory is full
        /// </summary>
        InventoryFull = 256,
        /// <summary>
        /// The item is not in the inventory
        /// </summary>
        ItemNotInInventory = 512,
        /// <summary>
        /// The item is not equippable
        /// </summary>
        ItemNotEquippable = 1024,
        /// <summary>
        /// The item has been equipped
        /// </summary>
        ItemEquipped = 2048,
        /// <summary>
        /// The item has been unequipped
        /// </summary>
        ItemUnequipped = 4096,
        /// <summary>
        /// The item has been used
        /// </summary>
        ItemUsed = 8192,
        /// <summary>
        /// The item is out of the inventory range (usually means that an indexing error has occurred)
        /// </summary>
        OutOfInventoryRange = 16384,
        /// <summary>
        /// The item is not interactable
        /// </summary>
        ItemNotInteractable = 32768,
        /// <summary>
        /// No item is selected
        /// </summary>
        NoItemSelected = 65536,

        /// <summary>
        /// The inventory is locked and can not be interacted with.
        /// </summary>
        InventoryLocked = 131072,
    }
}