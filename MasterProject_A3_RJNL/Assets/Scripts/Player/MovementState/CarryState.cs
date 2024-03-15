// Creator: Ruben
// Edited by:
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player.MovementState
{
    public class CarryState : IMovementState
    {
        private const string BASE_STATE_STRING = "baseState";

        public Type StateType { get; }

        private PlayerMovement playerMovement;
        private PlayerStats playerStats;
        private GuardHolder guardHolder;

        private int carrySpeed;

        public CarryState(PlayerMovement playerMovement, PlayerStats playerStats, GuardHolder guardHolder, int carrySpeed)
        {
            StateType = GetType();
            this.playerMovement = playerMovement;
            this.playerStats = playerStats;
            this.carrySpeed = carrySpeed;
        }

        public void EnterState()
        {
            playerMovement.UpdateMovementSpeedModifier(carrySpeed);
        }

        public void UpdateState()
        {
            CheckForInput();
        }

        void CheckForInput()
        {
            if (Input.GetKey(KeyCode.F))
            {
                playerStats.ChangeState(BASE_STATE_STRING);
            }
        }

        public void ExitState()
        {
            playerMovement.ResetMovementSpeedModifier();
        }
    }
}
