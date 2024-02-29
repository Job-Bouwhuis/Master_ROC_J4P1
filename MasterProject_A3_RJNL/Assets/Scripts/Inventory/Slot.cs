// Creator: job
using ShadowUprising.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

namespace ShadowUprising.Inventory
{
    /// <summary>
    /// A slot in the inventory UI. make sure to call <see cref="Init(int, InventoryManager)"/> before using the slot.
    /// <br></br> if you are using the slot through the <see cref="InventoryManager"/>, ignore the above step.
    /// </summary>
    public class Slot : MonoBehaviour
    {
        [Tooltip("The index of the slot in the InventoryManager. DO NOT CHANGE")]
        public int index;
        [Tooltip("The item in the slot. null if empty")]
        public Item? item;

        /// <summary>
        /// Whether the slot is selected or not. only a visual change.<br></br>
        /// All functionality is handled by the <see cref="InventoryManager"/>.
        /// </summary>
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                selectionGraphic.enabled = value;
            }
        }

        [SerializeField, Header("References")] private Image graphic;
        [SerializeField] private Image selectionGraphic;
        [SerializeField] private TMP_Text countText;

        [SerializeField, Header("DEBUG - DO NOT CHANGE")] private bool isSelected = false;

        private InventoryManager manager;

        /// <summary>
        /// Do not call this method if you are using the slot through the <see cref="InventoryManager"/>
        /// </summary>
        /// <param name="slotIndex"></param>
        /// <param name="manager"></param>
        public void Init(int slotIndex, InventoryManager manager)
        {
            index = slotIndex;
            this.manager = manager;
            countText.text = "";
        }

        /// <summary>
        /// Sets the stack size graphic to the given <paramref name="count"/>
        /// </summary>
        /// <param name="count"></param>
        public void newStackSize(int count)
        {
            if (count <= 1)
            {
                countText.text = "";
            }
            else
            {
                countText.text = count.ToString();
            }
        }

        /// <summary>
        /// Sets the item in the slot to the given <paramref name="item"/>
        /// </summary>
        /// <param name="item"></param>
        public void SetITem(Item? item)
        {
            if (item == null)
            {
                Clear();
                return;
            }

            this.item = item;
            graphic.sprite = item.icon;
            countText.text = item.CurrentStackSize is 0 or 1 ? "" : item.CurrentStackSize.ToString();
            graphic.color = new Color(255, 255, 255, 255);
        }

        /// <summary>
        /// Clears the slot
        /// </summary>
        public void Clear()
        {
            graphic.sprite = null;
            graphic.color = new Color(0, 0, 0, 0);
            countText.text = "";
            item = null;
        }
    }
}