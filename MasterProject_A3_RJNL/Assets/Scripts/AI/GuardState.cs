//Creator: Luke
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    public class GuardState : MonoBehaviour
    {
        public AIState currentState;
        public Action<AIState> onStateChanged = delegate { };

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