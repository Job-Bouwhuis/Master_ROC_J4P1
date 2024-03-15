// Creator: Ruben
using ShadowUprising;
using ShadowUprising.Player;
using ShadowUprising.WorldInteraction;
using UnityEngine;

namespace ShadowUprising.AI.Bodies
{
    public class GuardDeadBodies : MonoBehaviour, IWorldInteractable
    {
        public GameObject guardBody;
        GuardHolder guardHolder;

        public int Priority => 0;

        private void Start()
        {
            // potentially inefficient but simplest solution
            guardHolder = FindObjectOfType<GuardHolder>();
            if (guardHolder == null)
                Log.PushWarning("Dead Guard unable to find guardHolder component (which should be contained within in player). Without this component in the scene the player will be unable to pick up dead guards");
        }

        public void Interact(WorldInteractor interactor)
        {
            // TODO: ADD InventoryManager.Instance.lock();
            if (guardHolder == null)
            {
                Log.PushError("Player cannot pickup dead guard because GuardHolder cannot be found in scene. this object should be contained within the player");
                return;
            }
            guardHolder.GrabGuard(guardBody);
        }
    }
}