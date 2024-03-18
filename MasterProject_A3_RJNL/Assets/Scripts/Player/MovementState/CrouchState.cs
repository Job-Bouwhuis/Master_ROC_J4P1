// Creator: Ruben
// Edited by:
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player.MovementState
{
    public class CrouchState : IMovementState
    {
        private const string BASE_STATE_STRING = "baseState";

        public Type StateType { get; }

        private PlayerMovement playerMovement;
        private PlayerStats playerStats;
        private Transform cameraTransform;

        private float defaultCameraHeight;
        private int crouchSpeed;
        private float crouchCameraHeight;

        public CrouchState(PlayerMovement playerMovement, PlayerStats playerStats, Transform cameraTransform, int crouchSpeed, float crouchCameraHeight)
        {
            StateType = GetType();
            this.playerMovement = playerMovement;
            this.playerStats = playerStats;
            this.cameraTransform = cameraTransform;
            this.crouchSpeed = crouchSpeed;
            this.crouchCameraHeight = crouchCameraHeight;
        }

        public void EnterState()
        {
            defaultCameraHeight = cameraTransform.localPosition.y;
            playerMovement.UpdateMovementSpeedModifier(crouchSpeed);
            cameraTransform.localPosition = new Vector3(0, crouchCameraHeight, 0);
        }

        public void UpdateState()
        {
            CheckForInput();
        }

        void CheckForInput()
        {
            if (!Input.GetKey(KeyCode.LeftControl))
                playerStats.ChangeState(BASE_STATE_STRING);
        }

        public void ExitState()
        {
            playerMovement.ResetMovementSpeedModifier();
            cameraTransform.localPosition = new Vector3(0, defaultCameraHeight, 0);
        }
    }
}
