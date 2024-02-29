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

        Dictionary<string, MovementState.IMovementState> movementStates = new Dictionary<string, MovementState.IMovementState>();
        MovementState.IMovementState currentMovementState;

        private void Start()
        {
            pm = GetComponent<PlayerMovement>();
            InitializeMovementStates();
            currentMovementState = movementStates["baseState"];
        }

        private void InitializeMovementStates()
        {
            movementStates.Add("baseState", new MovementState.BaseState());
            movementStates.Add("crouchState", new MovementState.CrouchState(pm, this, cameraTransform));
            movementStates.Add("sprintState", new MovementState.SprintState(pm, this));
        }

        private void Update()
        {
            CheckForStateInputs();
            currentMovementState.UpdateState();
            UpdateStaminaRegen();
        }

        void CheckForStateInputs()
        {
            // Crouching has priority before sprinting
            // States only get reverted to the default within the states themselves to avoid states (primarily sprinting) to reactivate with same keypress
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                ChangeState("crouchState");
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > minimumStaminaToSprint)
            {
                ChangeState("sprintState");
            }
        }

        public void ChangeState(string state)
        {
            if (currentMovementState.GetType().Name == state)
                return;
            currentMovementState.ExitState();
            currentMovementState = movementStates[state];
            currentMovementState.EnterState();
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