// Creator: Ruben
// Edited by:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player.MovementState
{
    public interface IMovementState
    {
        public void EnterState();

        public void UpdateState();

        public void ExitState();
    }
}