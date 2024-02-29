using ShadowUprising.Inventory;
using UnityEngine;

namespace ShadowUprising.UI.InGame
{
    public class AmmoDisplayer : MonoBehaviour
    {
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
