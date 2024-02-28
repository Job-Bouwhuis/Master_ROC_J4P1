// Creator: Ruben
// Edited by:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Tooltip("The max health the player can have")]
        [SerializeField] private int maxHealth;
        [Tooltip("The player's Current Health")]
        public int health;
        [Tooltip("The max stamina the player can have")]
        [SerializeField] private int maxStamina;
        [Tooltip("The player's stamina, Gets utilized while sprinting")]
        public int stamina;

        MovementState.IMovementState movementState = new MovementState.BaseState();

        private void Update()
        {
            UpdateStates();
        }

        void UpdateStates()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                ChangeState(new MovementState.CrouchState(GetComponent<PlayerMovement>()));
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                ChangeState(new MovementState.SprintState());
            }
            else if (!(movementState is MovementState.BaseState))
            {
                ChangeState(new MovementState.BaseState());
            }
        }

        void ChangeState(MovementState.IMovementState state)
        {
            if (movementState.GetType() == state.GetType())
            {
                print("return");
                return;
            }
            print("change state");
            movementState.ExitState();
            movementState = state;
            movementState.EnterState();
        }
    }
}