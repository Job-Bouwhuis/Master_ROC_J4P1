using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.WorldInteraction
{
    public class DestroyOnInteract : MonoBehaviour, IWorldInteractable
    {
        public int Priority => -1;

        public void Interact(WorldInteractor interactor)
        {
            Destroy(gameObject);
        }
    }
}