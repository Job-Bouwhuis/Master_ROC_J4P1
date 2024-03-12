using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowUprising.AI;

namespace ShadowUprising.AI
{
    /// <summary>
    /// AIRoamingHandler handles the roaming of the ai
    /// </summary>
    public class AIRoamingHandler : MonoBehaviour
    {
        /// <summary>
        /// the roaming positions the ai should check
        /// </summary>
        public List<Transform> roamingPoints;

        /// <summary>
        /// determines the roaming state of the ai
        /// </summary>
        public RoamingState roamingState;

        AINavigationSystem aiSystem;
        Vector3 currentGoTo;

        bool roaming;
        int roamingIndex;
        const string toInvoke = "SetNewGoToPos";

        int direction = 1;

        // Start is called before the first frame update
        void Start()
        {         
            Asign();
            SetState(AIState.Roaming);
        }

        void StandingAtLocation()
        {
            if (roaming && !IsInvoking(toInvoke))
            {
                Invoke(toInvoke, 3.5f);
            }
        }

        void SetNewGoToPos()
        {
            currentGoTo = roamingPoints[NewPosIndex()].position;
            aiSystem.SetCurrentWayPoint(currentGoTo);
        }

        int NewPosIndex()
        {
            switch (roamingState)
            {
                case RoamingState.Random:
                    return Random.Range(0, roamingPoints.Count);
                case RoamingState.Loop:
                    if (roamingIndex == roamingPoints.Count - 1)
                        direction = -1;
                    else if (roamingIndex == 0)
                        direction = 1;
                    return roamingIndex += direction;
                case RoamingState.Sequential:
                    return roamingIndex++ % roamingPoints.Count;
                default:
                    return 0;
            }
        }

        void SetState(AIState currentState)
        {
            roaming = currentState == AIState.Roaming;
            if (roaming)
            {
                if (!IsInvoking(toInvoke))
                {
                    Invoke(toInvoke, 3.5f);
                }
            }

        }

        void Asign()
        {
            currentGoTo = roamingPoints[Random.RandomRange(0, roamingPoints.Count)].position;
            GetComponent<GuardState>().onStateChanged += SetState;
            aiSystem = GetComponent<AINavigationSystem>();
            GetComponent<AIMovementChecker>().onAIStanding += StandingAtLocation;
            GetComponent<AIMovementChecker>().onAIMoving += AIMovingToDestination;
        }

        private void AIMovingToDestination()
        {
            CancelInvoke(toInvoke);
        }
    }
}