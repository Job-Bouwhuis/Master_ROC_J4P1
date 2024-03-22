// Creator: Ruben
// Edited by:
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowUprising.Inventory;

namespace ShadowUprising.Player.MovementState
{
    /// <summary>
    /// MovementState used when carrying a guard. This state is only created when the GuardHolder Component exists within the player
    /// </summary>
    public class CarryState : IMovementState
    {
        private const string BASE_STATE_STRING = "baseState";

        /// <summary>
        /// The specific type of this class. Used to get the current state within other classes
        /// </summary>
        public Type StateType { get; }

        private PlayerMovement playerMovement;
        private PlayerStats playerStats;
        private GuardHolder guardHolder;

        private int carrySpeed;

        /// <summary>
        /// Constructor for the CarryState
        /// </summary>
        /// <param name="playerMovement">PlayerMovement Component within the player</param>
        /// <param name="playerStats">PlayerStats Component within the player</param>
        /// <param name="guardHolder">GuardHolder Component within the guard holder object. This should be the component that constructs this class</param>
        /// <param name="carrySpeed">The speed modifier applied to the player while carrying body</param>
        public CarryState(PlayerMovement playerMovement, PlayerStats playerStats, GuardHolder guardHolder, int carrySpeed)
        {
            StateType = GetType();
            this.playerMovement = playerMovement;
            this.playerStats = playerStats;
            this.guardHolder = guardHolder;
            this.carrySpeed = carrySpeed;
        }

        public void EnterState()
        {
            playerMovement.UpdateMovementSpeedModifier(carrySpeed);
            if (InventoryManager.Instance != null)
                InventoryManager.Instance.LockInventory();
        }

        public void UpdateState()
        {
            CheckForInput();
        }

        void CheckForInput()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                playerStats.ChangeState(BASE_STATE_STRING);
            }
        }

        public void ExitState()
        {
            guardHolder.DropGuard();
            playerMovement.ResetMovementSpeedModifier();
            if (InventoryManager.Instance != null)
                InventoryManager.Instance.UnlockInventory();
        }
    }
}
