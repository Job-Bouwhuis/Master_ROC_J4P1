//Creator: Luke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    /// <summary>
    /// handles al the animations of the Ai
    /// </summary>
    public class AIAnimationHandler : MonoBehaviour
    {
        Animator animator;
        AIState state;

        // Start is called before the first frame update
        void Start()
        {
            AsignComponents();
        }

        private void Update()
        {
            SetAnimationState();
        }

        void SetAnimationState()
        {
            switch (state)
            {
                case AIState.Attacking:
                    OnAIAttacking();
                    break;
                case AIState.Roaming:
                    OnAIRoaming();
                    break;
            }
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
            state = currentState;
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
