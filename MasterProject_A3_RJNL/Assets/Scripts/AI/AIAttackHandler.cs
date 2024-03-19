//Creator: Luke
//Edited: Ruben
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
            GetComponent<AIPlayerConeDetector>().onObjectDetected += OnObjectDetected;
            state = GetComponent<GuardState>();
            aiSystem = GetComponent<AINavigationSystem>();
            timer.elapsed += PlayerLost;
        }

        private void PlayerLost()
        {
            state.SetState(AIState.Roaming);
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

        void OnObjectDetected(GameObject gameObject)
        {
            SetGuardState();
            lastPlayerLoc = gameObject.transform.position;
            aiSystem.SetCurrentWayPoint(gameObject.transform.position);
            timer.ZeroTimer();
        }

    }
}