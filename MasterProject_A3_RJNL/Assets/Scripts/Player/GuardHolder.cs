// Creator: Ruben
// Edited by: Job

using ShadowUprising.AI.Bodies;
using UnityEngine;

namespace ShadowUprising.Player
{
    /// <summary>
    /// Component made for the Guard Holder object which should be a child of the camera inside of the player.
    /// This component manages the behaviour from the player that lets the player
    /// </summary>
    public class GuardHolder : MonoBehaviour
    {
        private const string CARRY_STATE_STRING = "carryState";

        [Tooltip("The prefab for a regular dead guard. This is used for when a new dead guard has to be Instantiated when the player drops a body")]
        [SerializeField] GameObject deadGuardPrefab;
        [Tooltip("The prefab for the guard viewmodel object. This gets used when the player picks up a guard")]
        [SerializeField] GameObject deadGuardViemodelPrefab;
        [Tooltip("Variable that will override the movementSpeedModifier in playerMovement while player is carrying a dead guard")]
        [SerializeField] private int carrySpeedModifier;
        PlayerStats playerStats;
        GameObject heldGuard;

        /// <summary>
        /// Whether the player is currently holding a guard.
        /// </summary>
        public bool HasGuard => heldGuard != null;

        private void Start()
        {
            Assign();
        }

        void Assign()
        {
            playerStats = GetComponentInParent<PlayerStats>();
            playerStats.AddState(CARRY_STATE_STRING, new MovementState.CarryState(playerStats.GetComponent<PlayerMovement>(), playerStats, this, carrySpeedModifier));
            if (playerStats == null)
                Log.PushError("The GuardHolder is attached to an object which parent does not have a PlayerStats component or this component cannot be found. This component is REQUIRED for GuardHolder to work properly");

        }

        /// <summary>
        /// Function thats get called by the Dead Guard when interacted with to pick up the guard.
        /// This function instantiates the viewmodel prefab, changes the movementState and calls to destroy the current dead guard
        /// </summary>
        /// <param name="currentGuard">The guard interacted with to call the function</param>
        public void GrabGuard(GuardDeadBodies currentGuard)
        {
            if (heldGuard != null)
                return;
            heldGuard = Instantiate(deadGuardViemodelPrefab, transform);
            playerStats.ChangeState(CARRY_STATE_STRING);
            currentGuard.DestroyGuard();
        }

        /// <summary>
        /// Drops the current guard onto the ground by instantiating a new guard where the viewmodel is held and destroys the viewmodel
        /// </summary>
        public void DropGuard()
        {
            Instantiate(deadGuardPrefab, transform.position, transform.rotation);
            Destroy(heldGuard);
        }
    }
}