using ShadowUprising.Inventory;
using UnityEngine;

namespace ShadowUprising.UI.InGame
{
    public class AmmoDisplayer : MonoBehaviour
    {
        public ElementAnimator AmmoElement;

        private void Start()
        {
            InventoryManager.Instance.OnInventoryInteract.AddListener(InventoryInteracted);
        }

        private void InventoryInteracted(InventoryManager.InventoryInteractResult result)
        {
            Log.Push("InteractionResult");
            if (result.Status.HasFlag(InventoryInteractionResult.Failure))
            {
                Log.Push("interaction was a failure");
                return;
            }

            if (result.Status.HasFlag(InventoryInteractionResult.ItemEquipped))
            {
                Log.Push("item was equipped");
                if(result.Item == null)
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
