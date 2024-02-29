// Creator: Ruben
// Edited by:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player.MovementState
{
    public class SprintState : IMovementState
    {
        private PlayerMovement pm;
        PlayerStats ps;
        Rigidbody rb;

        const int SPRINT_SPEED = 300;
        const float STAMINA_DRAIN_RATE = 60;
        const float VELOCITY_THRESHOLD = 0.5f;

        bool outOfStamina;

        public SprintState(PlayerMovement pm, PlayerStats ps)
        {
            this.pm = pm;
            this.ps = ps;
        }

        public void EnterState()
        {
            ps.SetStaminaRegen(false);
            pm.UpdateMovementSpeedModifier(SPRINT_SPEED);
            rb = ps.GetComponent<Rigidbody>();
        }

        public void UpdateState()
        {
            UpdateStamina();
            CheckForInput();
        }

        void UpdateStamina()
        {
            if (ps.stamina <= 0)
            {
                outOfStamina = true;
                pm.ResetMovementSpeedModifier();
            }
            if (!outOfStamina)
            {
                float velocity = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
                if (velocity >= VELOCITY_THRESHOLD)
                    ps.stamina -= STAMINA_DRAIN_RATE * Time.deltaTime;
            }
        }

        void CheckForInput()
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                ps.ChangeState("baseState");
        }

        public void ExitState()
        {
            pm.ResetMovementSpeedModifier();
            ps.SetStaminaRegen(true);
        }
    }
}
