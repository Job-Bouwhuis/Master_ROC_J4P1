// Creator: Ruben
// Edited by:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player.MovementState
{
    public class CrouchState : IMovementState
    {

        [SerializeField] private PlayerMovement pm;
        const int CROUCHSPEED = -100;
        public CrouchState(PlayerMovement pm)
        {
            this.pm = pm;
        }

        public void EnterState()
        {
            pm.UpdateMovementSpeedModifier(CROUCHSPEED);
        }

        public void ExitState()
        {
            pm.ResetMovementSpeedModifier();
        }
    }
}
