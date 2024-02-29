// Creator: Ruben
// Edited by:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player
{
    public class PlayerStats : MonoBehaviour
    {
        private PlayerMovement pm;
        [SerializeField] private Transform cameraTransform;

        [Tooltip("The max health the player can have")]
        [SerializeField] private int maxHealth;
        [Tooltip("The player's Current Health")]
        public int health;
        [Tooltip("The max stamina the player can have")]
        [SerializeField] private int maxStamina;
        [Tooltip("The player's stamina, Gets utilized while sprinting")]
        public float stamina;
        private bool regenStamina;
        [Tooltip("Amount of stamina gets back per second")]
        [SerializeField] int staminaRegenRate;
        [Tooltip("The minimum amount of stamina the player needs to have in order to sprint")]
        [SerializeField] int minimumStaminaToSprint;

        MovementState.IMovementState movementState = new MovementState.BaseState();

        private void Start()
        {
            pm = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            CheckForStateInputs();
            movementState.UpdateState();
            UpdateStaminaRegen();
        }

        void CheckForStateInputs()
        {
            // Crouching has priority before sprinting
            // States only get reverted to the default within the states themselves (primarily sprinting) to reactivate with same keypress
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                ChangeState(new MovementState.CrouchState(pm, this, cameraTransform));
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > minimumStaminaToSprint)
            {
                ChangeState(new MovementState.SprintState(pm, this));
            }
        }

        public void ChangeState(MovementState.IMovementState state)
        {
            if (movementState.GetType() == state.GetType())
            {
                return;
            }
            movementState.ExitState();
            movementState = state;
            movementState.EnterState();
        }

        void UpdateStaminaRegen()
        {
            if (regenStamina)
            {
                stamina += staminaRegenRate * Time.deltaTime;
                if (stamina >= maxStamina)
                {
                    SetStaminaRegen(false);
                    stamina = maxStamina;
                }
            }
        }

        public void SetStaminaRegen(bool regen)
        {
            regenStamina = regen;
        }
    }
}