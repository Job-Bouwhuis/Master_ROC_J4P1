//Creator: Job
using ShadowUprising.Inventory;
using UnityEngine;

namespace ShadowUprising.UI.InGame
{
    /// <summary>
    /// This class is responsible for displaying the ammo of the weapon of the player
    /// </summary>
    public class AmmoDisplayer : MonoBehaviour
    {
        [Tooltip("The ammo element in the UI")]
        public ElementAnimator AmmoElement;
        [Tooltip("The gun object")]
        public GameObject? Gun;

        private void Start()
        {
            InventoryManager.Instance.OnInventoryInteract += InventorySelected;

            Gun.SetActive(false);
        }

        private void InventorySelected(InventoryManager.InventoryInteractResult result)
        {
            if (result.Status.HasFlag(InventoryInteractionResult.Failure))
                return;

            if (result.Status.HasFlag(InventoryInteractionResult.ItemEquipped))
            {
                if (result.Item == null)
                {
                    AmmoElement.HideFromIndefinite();

                    if (Gun != null)
                        Gun.SetActive(false);
                    return;
                }

                if (result.Item.itemName == "Gun")
                {
                    AmmoElement.ShowIndefinite();
                    if(Gun != null)
                        Gun.SetActive(true);
                }
                else
                {
                    AmmoElement.HideFromIndefinite();
                    if (Gun != null)
                        Gun.SetActive(false);
                }
            }
        }
    }
}
