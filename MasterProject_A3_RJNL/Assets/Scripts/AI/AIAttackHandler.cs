//Creator: Luke
using UnityEngine;
using ShadowUprising.Utils;

namespace ShadowUprising.AI
{
    /// <summary>
    /// handles the attacking for the ai
    /// </summary>
    public class AIAttackHandler : MonoBehaviour
    {

        /// <summary>
        /// the distance to the last player location until the ai can go back to roaming
        /// </summary>
        public float distanceToLastPlayerLoc;

        GuardState state;
        AINavigationSystem aiSystem;
        Timer timer = new Timer(3000);
        Vector3 lastPlayerLoc;

    // Start is called before the first frame update
        void Start()
        {
            Asign();
        }

        private void Update()
        {
            timer.Update(Time.deltaTime);
            CheckForReturnToRoaming();
        }

        void Asign()
        {
            GetComponent<AIPlayerConeDetector>().onPlayerDetected += OnPlayerDetected;
            state = GetComponent<GuardState>();
            aiSystem = GetComponent<AINavigationSystem>();
            timer.elapsed += PlayerLost;
        }

        private void PlayerLost()
        {
            state.SetState(AIState.Roaming);
        }

        void SetGuardState()
        {
            if (state.CurrentState != AIState.Attacking)
                state.SetState(AIState.Attacking);
        }

        void CheckForReturnToRoaming()
        {
            if (IsAIAtLocation())
                ReturnToRoaming();
        }

        bool IsAIAtLocation()
        {
            if (Vector3.Distance(lastPlayerLoc, transform.position) < distanceToLastPlayerLoc)
                return true;
            return false;
        }

        void ReturnToRoaming()
        {
            if (state.CurrentState == AIState.Attacking)
                timer.StartTimer();
        }

        void OnPlayerDetected(Vector3 playerPos)
        {
            SetGuardState();
            lastPlayerLoc = playerPos;
            aiSystem.SetCurrentWayPoint(playerPos);
            timer.ZeroTimer();
        }

    }
}