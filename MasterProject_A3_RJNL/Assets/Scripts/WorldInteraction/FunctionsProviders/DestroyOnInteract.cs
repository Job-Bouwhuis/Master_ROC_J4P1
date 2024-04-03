// Creator: Job
using UnityEngine;

namespace ShadowUprising.WorldInteraction
{
    /// <summary>
    /// World interactable that destroys the game object when interacted with.
    /// </summary>
    public class DestroyOnInteract : MonoBehaviour, IWorldInteractable
    {
        public int Priority => -1;

        public void Interact(WorldInteractor interactor)
        {
            Destroy(gameObject);
        }
    }
}