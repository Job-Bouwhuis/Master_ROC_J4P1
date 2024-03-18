using System.Collections;
using ShadowUprising;
using ShadowUprising.Detection;
using ShadowUprising.AI.Bodies;
using UnityEngine;

namespace ShadowUprising.Player
{
    public class GuardHolder : MonoBehaviour
    {
        private const string CARRY_STATE_STRING = "carryState";

        [SerializeField] GameObject deadGuardPrefab;
        [SerializeField] GameObject deadGuardViemodelPrefab;
        [Tooltip("Variable that will override the movementSpeedModifier in playerMovement while player is carrying a dead guard")]
        [SerializeField] private int carrySpeedModifier;
        PlayerStats playerStats;
        GameObject heldGuard;

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

        public void GrabGuard(GuardDeadBodies currentGuard)
        {
            if (heldGuard != null)
                return;
            heldGuard = Instantiate(deadGuardViemodelPrefab, transform);
            playerStats.ChangeState(CARRY_STATE_STRING);
            currentGuard.DestroyGuard();
        }

        public void DropGuard()
        {
            Instantiate(deadGuardPrefab, transform.position, transform.rotation);
            Destroy(heldGuard);
        }
    }
}