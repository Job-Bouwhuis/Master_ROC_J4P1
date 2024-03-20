//Creator: Job
//Edited: Ruben
using ShadowUprising.Inventory;
using UnityEngine;

namespace ShadowUprising.UI.InGame
{
    public class GunModelDisplayer : MonoBehaviour
    { 
        [Tooltip("The gun object")]
        public GameObject Gun;

        private void Start()
        {
            Gun.SetActive(false);

            if (InventoryManager.Instance == null)
                return;
            InventoryManager.Instance.OnInventoryInteract.AddListener(InventorySelected);
            InventoryManager.Instance.lockInventory += LockInventory;
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