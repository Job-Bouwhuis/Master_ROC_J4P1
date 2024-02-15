using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowUprising.AI;

namespace ShadowUprising.AI
{
    public class AIRoamingHandler : MonoBehaviour
    {
        public List<Transform> roamingPoints;

        AINavigationSystem aiSystem;
        Vector3 currentGoTo;

        bool roaming;


        // Start is called before the first frame update
        void Start()
        {
            Asign();
        }

        void StandingAtLocation()
        {
            if (roaming)
                if (!IsInvoking("SetNewGoToPos"))
                    Invoke("SetNewGoToPos", 3.5f);
        }

        void AIMovingToDestination()
        {
            CancelInvoke("SetNewGoToPos");
        }

        void SetNewGoToPos()
        {
            currentGoTo = roamingPoints[Random.RandomRange(0, roamingPoints.Count)].position;
            aiSystem.SetCurrentWayPoint(currentGoTo);
        }

        void SetState(AIState currentState)
        {
            roaming = currentState == AIState.Roaming;
            if (!roaming)
            {
                if (IsInvoking("SetNewGoToPos"))
                    CancelInvoke("SetNewGoToPos");
            }
            else
                SetNewGoToPos();

        }

        void Asign()
        {
            currentGoTo = roamingPoints[Random.RandomRange(0, roamingPoints.Count)].position;
            GetComponent<GuardState>().onStateChanged += SetState;
            aiSystem = GetComponent<AINavigationSystem>();
            GetComponent<AIMovementChecker>().onAIStanding += StandingAtLocation;
            GetComponent<AIMovementChecker>().onAIMoving += AIMovingToDestination;
        }

    }
}