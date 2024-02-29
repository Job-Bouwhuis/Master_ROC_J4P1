// Creator: Ruben
// Edited by:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player.MovementState
{
    public class CrouchState : IMovementState
    {

        private PlayerMovement pm;
        private PlayerStats ps;
        private Transform cameraTransform;

        private float defaultCameraHeight; 

        const int CROUCH_SPEED = -130;
        const float CROUCH_CAMERA_HEIGHT = -0.10f;

        public CrouchState(PlayerMovement pm, PlayerStats ps, Transform cameraTransform)
        {
            this.pm = pm;
            this.ps = ps;
            this.cameraTransform = cameraTransform;
        }

        public void EnterState()
        {
            defaultCameraHeight = cameraTransform.localPosition.y;
            pm.UpdateMovementSpeedModifier(CROUCH_SPEED);
            cameraTransform.localPosition = new Vector3(0, CROUCH_CAMERA_HEIGHT, 0);
        }

        public void UpdateState()
        {
            CheckForInput();
        }

        void CheckForInput()
        {
            if (!Input.GetKey(KeyCode.LeftControl))
                ps.ChangeState(new BaseState());
        }

        public void ExitState()
        {
            pm.ResetMovementSpeedModifier();
            cameraTransform.localPosition = new Vector3(0, defaultCameraHeight, 0);
        }
    }
}
