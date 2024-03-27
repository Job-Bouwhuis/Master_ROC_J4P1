//Creator: Job
//Edited: Ruben
using ShadowUprising.Inventory;
using UnityEngine;

namespace ShadowUprising.UI.InGame
{
    /// <summary>
    /// Component to display the gun model when the gun is equipped
    /// </summary>
    public class GunModelDisplayer : MonoBehaviour
    { 
        [Tooltip("The gun object")]
        public GameObject Gun;

        private void Start()
        {
            Gun.SetActive(false);

            if (InventoryManager.Instance == null)
                return;
            InventoryManager.Instance.OnInventoryInteract += InventorySelected;
            InventoryManager.Instance.OnInventoryLockChange += LockInventory;
        }

        private void InventorySelected(InventoryManager.InventoryInteractResult result)
        {
            if (result.Status.HasFlag(InventoryInteractionResult.Failure))
                return;

            if (result.Status.HasFlag(InventoryInteractionResult.ItemEquipped))
            {
                if (result.Item == null)
                {

                    if (Gun != null)
                        Gun.SetActive(false);
                    return;
                }

                if (result.Item.itemName == "Gun")
                {
                    if (Gun != null)
                        Gun.SetActive(true);
                }
                else
                {
                    if (Gun != null)
                        Gun.SetActive(false);
                }
            }
        }

        private void LockInventory(bool value)
        {
            Gun.SetActive(!value);
        }
    }
}