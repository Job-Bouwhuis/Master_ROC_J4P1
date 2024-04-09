using ShadowUprising.UI.InfoDialogs;
using ShadowUprising.WorldInteraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.WorldInteraction.InteractableItems
{
    public class NightVisionPickup : MonoBehaviour, IWorldInteractable
    {
        public int Priority => 0;

        public void Interact(WorldInteractor interactor)
        {
            // unlock nv
            FindObjectOfType<NighVision.NighVisionHandler>().pickedUp = true;
            InfoDialogManager.Instance.ShowInfoDialog("Night Vision", "Press V to toggle night vision!", 8);
            Destroy(gameObject);
        }
    }
}