// Creator: Ruben
// Edited by:
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player.MovementState
{
    public class BaseState : IMovementState
    {
        public Type StateType { get; }

        public BaseState()
        {
            StateType = GetType();
        }

        public void EnterState()
        {

        }

        public void UpdateState()
        {

        }

        public void ExitState()
        {

        }
    }
}
