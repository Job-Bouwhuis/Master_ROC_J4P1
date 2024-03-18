// Creator: Ruben
using ShadowUprising;
using ShadowUprising.Player;
using ShadowUprising.Inventory;
using ShadowUprising.WorldInteraction;
using ShadowUprising.Detection;
using UnityEngine;

namespace ShadowUprising.AI.Bodies
{
    public class GuardDeadBodies : MonoBehaviour, IWorldInteractable
    {
        public GameObject guardBody;
        DetectableObjects detectableObjects;
        GuardHolder guardHolder;

        public int Priority => 0;

        private void Start()
        {
            Assign();
        }

        void Assign()
        {
            // The code in this function is pretty badly optimized. this gets called everytime a new dead guard spawns and is quite intensive
            // This will be optimised if the time is found. I currently do not have the time to optimise this - Ruben
            guardHolder = FindObjectOfType<GuardHolder>();
            if (guardHolder == null)
                Log.PushWarning("Dead Guard unable to find guardHolder component (which should be contained within in player). Without this component in the scene the player will be unable to pick up dead guards");

            detectableObjects = FindObjectOfType<DetectableObjects>();
            if (detectableObjects == null)
                Log.PushWarning("Cannot find DetectableObjects component in scene. This will result that current dead guard will not be added to the detectableObjects list. This component is usually on the DetectionManager object. This object should have a prefab thats easy to add");
            detectableObjects.AddDetectableObject(this.gameObject);
        }

        public void Interact(WorldInteractor interactor)
        {
            // TODO: ADD InventoryManager.Instance.lock();
            //if (InventoryManager.Instance != null)
            //    InventoryManager.Instance.;

            if (guardHolder == null)
            {
                Log.PushError("Player cannot pickup dead guard because GuardHolder cannot be found in scene. this object should be contained within the player");
                return;
            }
            guardHolder.GrabGuard(this);
        }

        public void DestroyGuard()
        {
            if (detectableObjects != null)
                detectableObjects.RemoveDetectableObject(this.gameObject);
            Destroy(guardBody);
        }    
    }
}