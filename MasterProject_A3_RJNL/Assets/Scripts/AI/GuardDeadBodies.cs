// Creator: Ruben
using ShadowUprising;
using ShadowUprising.Player;
using ShadowUprising.Inventory;
using ShadowUprising.WorldInteraction;
using ShadowUprising.Detection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShadowUprising.AI.Bodies
{
    /// <summary>
    /// This class is to be applied to the dead bodies of guard. This script manages the player interaction with bodies, letting the player pick up the bodies.
    /// </summary>
    public class GuardDeadBodies : MonoBehaviour, IWorldInteractable
    {
        [Tooltip("Parent of all components in the Guard Ragdoll. Used to destroy entire prefab when needed")]
        public GameObject guardBody;
        DetectableObjects detectableObjects;
        GuardHolder guardHolder;

        /// <summary>
        /// The priority of the object for the player interaction
        /// </summary>
        public int Priority => 0;

        private void Start()
        {
            if(SceneManager.GetActiveScene().name == "DEMOEND")
            {
                return;
            }
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

        /// <summary>
        /// Function that gets callled by the WorldInteractor system when the player looks at the body and presses the F key
        /// This function calls the Guard Holder within the player letting that component manage the player behavior relating to picking up a body
        /// </summary>
        /// <param name="interactor">The WorldInteractor component that called this function</param>
        public void Interact(WorldInteractor interactor)
        {
            if (guardHolder == null)
            {
                Log.PushError("Player cannot pickup dead guard because GuardHolder cannot be found in scene. this object should be contained within the player");
                return;
            }
            guardHolder.GrabGuard(this);
        }

        /// <summary>
        /// Function called that destroys the current guard and removes it from the list of detectableObjects
        /// </summary>
        public void DestroyGuard()
        {
            if (detectableObjects != null)
                detectableObjects.RemoveDetectableObject(this.gameObject);
            Destroy(guardBody);
        }    
    }
}