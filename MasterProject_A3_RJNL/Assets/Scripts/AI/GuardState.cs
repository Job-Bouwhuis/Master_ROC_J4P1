//Creator: Luke
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    /// <summary>
    /// is the guard state of the ai guard
    /// </summary>
    public class GuardState : MonoBehaviour
    {
        /// <summary>
        /// is called when the state is changed
        /// </summary>
        public Action<AIState> onStateChanged = delegate { };

        /// <summary>
        /// current state of the ai
        /// </summary>
        public AIState CurrentState => currentState;
        [SerializeField] private AIState currentState = AIState.Roaming;
        

        /// <summary>
        /// sets the current guard state
        /// </summary>
        /// <param name="current">current state it has to be</param>
        public void SetState(AIState current)
        {
            currentState = current;
            onStateChanged.Invoke(currentState);
        }
    }
}