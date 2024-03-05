// Creator: Ruben
// Edited by:
using ShadowUprising.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.Player
{
    public class MouseCamera : MonoBehaviour
    {
        /// <summary>
        /// Transform needs to be declared because this component is not a Monobehaviour
        /// </summary>
        [SerializeField] Transform cameraTransform;

        [SerializeField] private int horizontalSensitivity;
        [SerializeField] private int verticalSensitivity;
        const int CLAMP = 85;

        private float rotateHorizontal;
        private float rotateVertical;

        private void Awake()
        {
            horizontalSensitivity = verticalSensitivity = GameSettings.Instance.sensitivity.FloorToInt();
            LockMouse();
        }

        void Update()
        {

            UpdateCamera();
        }

        void UpdateCamera()
        {
            UpdateHorizontal();
            UpdateVertical();
        }

        /// <summary>
        /// Update the horizontal rotation of the player based on player input
        /// </summary>
        public void UpdateHorizontal()
        {
            rotateHorizontal += Input.GetAxis("Mouse X") * Time.deltaTime * horizontalSensitivity;
            transform.eulerAngles = new Vector3(0, rotateHorizontal, 0);
        }

        /// <summary>
        /// Update the vertical rotation of the camera based on player input
        /// </summary>
        public void UpdateVertical()
        {
            rotateVertical -= Input.GetAxis("Mouse Y") * Time.deltaTime * verticalSensitivity;
            rotateVertical = Mathf.Clamp(rotateVertical, -CLAMP, CLAMP);
            cameraTransform.localRotation = Quaternion.Euler(rotateVertical, 0, 0);
        }

        /// <summary>
        /// Set curserlockstate to locked and hide the mouse
        /// </summary>
        void LockMouse()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}