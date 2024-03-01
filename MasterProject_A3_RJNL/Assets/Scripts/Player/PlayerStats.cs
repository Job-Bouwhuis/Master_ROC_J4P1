// Creator: Ruben
// Edited by:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerStats : MonoBehaviour
    {
        private PlayerMovement playerMovement;
        [SerializeField] private Transform cameraTransform;

        private const string BASE_STATE = "baseState";
        private const string CROUCH_STATE = "crouchState";
        private const string SPRINT_STATE = "sprintState";

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
        [SerializeField] private int staminaRegenRate;
        [Tooltip("The minimum amount of stamina the player needs to have in order to sprint")]
        [SerializeField] private int minimumStaminaToSprint;
        [Tooltip("Variable that will override the movementSpeedModifier in playerMovement while crouching (Should be a negative number)")]
        [SerializeField] private int crouchSpeedModifier;
        [Tooltip("Y value of camera's localtransform when entering the crouch state")]
        [SerializeField] private float crouchCameraHeight;
        [Tooltip("Variable that will override the movementSpeedModifier in playerMovement while sprinting")]
        [SerializeField] private int sprintSpeedModifier;
        [Tooltip("Amount of stamina drained per second while sprinting")]
        [SerializeField] private float staminaDrainRate;

        Dictionary<string, MovementState.IMovementState> movementStates = new Dictionary<string, MovementState.IMovementState>();
        MovementState.IMovementState currentMovementState;

        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
            InitializeMovementStates();
            currentMovementState = movementStates[BASE_STATE];
        }

        private void InitializeMovementStates()
        {
            movementStates.Add(BASE_STATE, new MovementState.BaseState());
            movementStates.Add(CROUCH_STATE, new MovementState.CrouchState(playerMovement, this, cameraTransform, crouchSpeedModifier, crouchCameraHeight));
            movementStates.Add(SPRINT_STATE, new MovementState.SprintState(playerMovement, this, sprintSpeedModifier, staminaDrainRate));
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
                ChangeState(CROUCH_STATE);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > minimumStaminaToSprint)
            {
                ChangeState(SPRINT_STATE);
            }
        }

        /// <summary>
        /// Change the current MovementState of the player
        /// </summary>
        /// <param name="state">Name of target stateW</param>
        public void ChangeState(string state)
        {
            if (currentMovementState.StateType.Name == state)
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

        /// <summary>
        /// Toggle the passive regeneration of the stamina variable
        /// </summary>
        public void SetStaminaRegen(bool regen)
        {
            regenStamina = regen;
        }
    }
}