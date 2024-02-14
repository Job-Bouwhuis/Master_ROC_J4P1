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
                {
                    return itemFunction;
                }

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
        [SerializeField, Tooltip("Can be set to a different value to have a different starting stack size. eg, you pick up 2 shoes instead of one when picking up shoes")] private int currentStackSize = 1;

        /// <summary>
        /// Whether or not the item can be stacked
        /// </summary>
        public bool IsStackable => maxStackSize > 1;

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
    }
}