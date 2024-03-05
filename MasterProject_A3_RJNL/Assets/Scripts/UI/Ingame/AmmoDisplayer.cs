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

        private void Start()
        {
            InventoryManager.Instance.OnInventoryInteract.AddListener(InventorySelected);
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
                    return;
                }

                if (result.Item.itemName == "Gun")
                {
                    AmmoElement.ShowIndefinite();
                }
                else
                    AmmoElement.HideFromIndefinite();
            }
        }
    }
}
