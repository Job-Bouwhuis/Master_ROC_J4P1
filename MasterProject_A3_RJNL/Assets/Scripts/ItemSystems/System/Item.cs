// Creator: job

using System;
using UnityEngine;
using WinterRose;

#nullable enable

namespace ShadowUprising.Items
{
    /// <summary>
    /// The base class for all items. Make sure to fill in the stuff like name and description
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
    public class Item : ScriptableObject
    {
        public enum ItemInteractButton
        {
            LeftClick,
            RightClick,
            MiddleClick,
            E,
            None
        }

        /// <summary>
        /// The unique index ID within the <see cref="Inventory.InventoryManager.allItems"/> that this item finds itself
        /// </summary>
        [NonSerialized] public int id = -1;

        /// <summary>
        /// The name of the item
        /// </summary>
        public string itemName;
        /// <summary>
        /// a description of the item
        /// </summary>
        public string description;
        /// <summary>
        /// The icon of the item
        /// </summary>
        public Sprite icon;
        /// <summary>
        /// The prefab of the item.
        /// </summary>
        [Tooltip("The game object that should be used for this specific item")]
        public GameObject prefab;

        [Tooltip("The button that will be used to interact with the item when selected in the inventory")]
        public ItemInteractButton interactButton = ItemInteractButton.E;


        /// <summary>
        /// The function that the item will execute when used. can be null to have no function
        /// </summary>
        public IItemFunction? ItemFunction
        {
            get
            {   
                if (itemFunction is not null)
                {
                    return itemFunction;
                }
                if (string.IsNullOrEmpty(ItemFunctionProviderName))
                {
                    return itemFunction;
                }

                Type type = TypeWorker.FindType(ItemFunctionProviderName);
                if (type == null)
                    return itemFunction;

                // find if a component exists in the scene with the same type
                if (type.IsAssignableTo(typeof(UnityEngine.Object)))
                {
                    var component = FindObjectOfType(type);
                    if (component != null)
                    {
                        itemFunction = (IItemFunction)component;
                    }
                    else
                    {
                        itemFunction = (IItemFunction)Activator.CreateInstance(type);
                    }
                }

                return itemFunction;
            }
        }
        private IItemFunction? itemFunction;

        /// <summary>
        /// The maximum amount of items that can be stacked
        /// </summary>
        public int MaxStackSize => maxStackSize;
        [SerializeField] private int maxStackSize = 1;

        /// <summary>
        /// The current amount of items in the stack
        /// </summary>
        public int CurrentStackSize => currentStackSize;
        [SerializeField, Tooltip("Can be set to a different value to have a different starting stack size. eg, you start with 2 shoes instead of one")] private int currentStackSize = 1;

        /// <summary>
        /// The amount of items that will be given when the item is given. this is used for things like picking up 2 shoes instead of one
        /// </summary>
        [Tooltip("The amount of items that will be given when the item is given. this is used for things like picking up 2 shoes instead of one")]
        public int itemGiveIncrement = 1;

        /// <summary>
        /// Whether or not the item can be stacked
        /// </summary>
        public bool IsStackable => maxStackSize > 1;

        /// <summary>
        /// Whether or not the item has a function
        /// </summary>
        public bool HasFunction => ItemFunction != null;

        /// <summary>
        /// The type name of the item function provider. this is assigned by in the editor.<br></br><br></br>
        /// 
        /// <b>DO NOT CHANGE THROUGH CODE</b>
        /// </summary>
        [HideInInspector] public string ItemFunctionProviderName = "";

        /// <summary>
        /// <b>This is only be used by the <see cref="Inventory.InventoryManager"/>. Must you still use this, discuss with Job</b>
        /// </summary>
        /// <param name="newStackSize"></param>
        public void SetStack(int newStackSize)
        {
            currentStackSize = newStackSize;
        }

        internal Item Copy()
        {
            Item newItem = CreateInstance<Item>();
            newItem.itemName = itemName;
            newItem.description = description;
            newItem.icon = icon;
            newItem.maxStackSize = maxStackSize;
            newItem.currentStackSize = currentStackSize;
            newItem.itemGiveIncrement = itemGiveIncrement;
            newItem.ItemFunctionProviderName = ItemFunctionProviderName;
            newItem.interactButton = interactButton;
            newItem.id = id;

            return newItem;
        }

        /// <summary>
        /// Resets <see cref="ItemFunction"/> to null so it re-assigns the function. <br></br>
        /// Usefull when the item is being loaded from a save file or when the item is being percisted between scenes
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void ResetFunction()
        {
            itemFunction = null;
        }
    }
}