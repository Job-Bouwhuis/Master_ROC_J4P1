using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{


    public class AIAnimationHandler : MonoBehaviour
    {
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            AsignComponents();
        }

        void OnAIMoving()
        {
            animator.SetTrigger("Moving");
        }

        void OnAIStanding()
        {
            animator.SetTrigger("Standing");
        }

        void OnAIAttacking()
        {
            animator.SetTrigger("Attacking");
        }

        void OnAIRoaming()
        {
            animator.SetTrigger("Roaming");
        }

        void OnStateChanged(AIState currentState)
        {
            switch (currentState)
            {
                case AIState.Attacking:
                    OnAIAttacking();
                    break;
                case AIState.Roaming:
                    OnAIRoaming();
                    break;
            }
        }
        

        void AsignComponents()
        {
            animator = GetComponent<Animator>();
            var comp = GetComponent<AIMovementChecker>();
            comp.onAIMoving += OnAIMoving;
            comp.onAIStanding += OnAIStanding;
            var state = GetComponent<GuardState>();
            state.onStateChanged += OnStateChanged;

        }

    }
}
