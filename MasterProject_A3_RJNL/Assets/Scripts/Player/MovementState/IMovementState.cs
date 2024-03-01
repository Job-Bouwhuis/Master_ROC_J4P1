// Creator: Ruben
// Edited by:
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player.MovementState
{
    public interface IMovementState
    {
        public Type StateType { get; }

        /// <summary>
        /// Function to be executed everytime this state gets activated
        /// </summary>
        public void EnterState();

        /// <summary>
        /// Function to be executed every frame this state is active
        /// </summary>
        public void UpdateState();

        /// <summary>
        /// Function to be executed when this state becomes deactivated
        /// </summary>
        public void ExitState();
    }
}