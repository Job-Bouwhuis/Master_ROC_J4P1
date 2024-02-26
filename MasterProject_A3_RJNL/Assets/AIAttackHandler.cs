using ShadowUprising.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowUprising.Utils;

namespace ShadowUprising.AI
{

    public class AIAttackHandler : MonoBehaviour
    {
        GuardState state;
        AINavigationSystem aiSystem;
        Timer timer = new Timer(3000);
    
    // Start is called before the first frame update
        void Start()
        {
            Asign();
        }

        private void Update()
        {
            timer.Update(Time.deltaTime);
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
            state.currentState = AIState.Roaming;
        }

        void SetGuardState()
        {
            if (state.currentState != AIState.Attacking)
                state.SetState(AIState.Attacking);
        }

        void OnPlayerDetected(Vector3 playerPos)
        {
            SetGuardState();
            aiSystem.SetCurrentWayPoint(playerPos);           
            timer.Restart();
        }

    }
}