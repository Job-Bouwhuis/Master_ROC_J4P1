// Creator: Ruben
// Edited by:
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player.MovementState
{
    public class SprintState : IMovementState
    {
        private const string BASE_STATE_STRING = "baseState";

        public Type StateType { get; }

        private PlayerMovement playerMovement;
        PlayerStats playerStats;
        Rigidbody rigidbody;

        const float VELOCITY_THRESHOLD = 0.5f;

        private int sprintSpeed;
        private float staminaDrainRate;
        private bool outOfStamina;

        public SprintState(PlayerMovement playerMovement, PlayerStats playerStats, int sprintSpeed, float staminaDrainRate)
        {
            StateType = GetType();
            this.playerMovement = playerMovement;
            this.playerStats = playerStats;
            this.sprintSpeed = sprintSpeed;
            this.staminaDrainRate = staminaDrainRate;
        }

        public void EnterState()
        {
            playerStats.SetStaminaRegen(false);
            playerMovement.UpdateMovementSpeedModifier(sprintSpeed);
            rigidbody = playerStats.GetComponent<Rigidbody>();
            outOfStamina = false;
        }

        public void UpdateState()
        {
            UpdateStamina();
            CheckForInput();
        }

        void UpdateStamina()
        {
            if (playerStats.stamina <= 0)
            {
                outOfStamina = true;
                playerMovement.ResetMovementSpeedModifier();
            }
            if (!outOfStamina)
            {
                float velocity = Mathf.Abs(rigidbody.velocity.x) + Mathf.Abs(rigidbody.velocity.y);
                if (velocity >= VELOCITY_THRESHOLD)
                    playerStats.stamina -= staminaDrainRate * Time.deltaTime;
            }
        }

        void CheckForInput()
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                playerStats.ChangeState(BASE_STATE_STRING);
        }

        public void ExitState()
        {
            playerMovement.ResetMovementSpeedModifier();
            playerStats.SetStaminaRegen(true);
        }
    }
}
