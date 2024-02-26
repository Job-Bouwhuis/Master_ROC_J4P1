// Creator: Ruben
// Edited by:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        void Update()
        {
            LockMouse();
            UpdateCamera();
        }

        void UpdateCamera()
        {
            UpdateHorizontal();
            UpdateVertical();
        }

        /// <summary>
        /// Update horizontal camera movement
        /// </summary>
        public void UpdateHorizontal()
        {
            rotateHorizontal += Input.GetAxis("Mouse X") * Time.deltaTime * horizontalSensitivity;
            transform.eulerAngles = new Vector3(0, rotateHorizontal, 0);
        }

        /// <summary>
        /// update vertical camera movement
        /// </summary>
        public void UpdateVertical()
        {
            rotateVertical -= Input.GetAxis("Mouse Y") * Time.deltaTime * verticalSensitivity;
            rotateVertical = Mathf.Clamp(rotateVertical, -CLAMP, CLAMP);
            cameraTransform.localRotation = Quaternion.Euler(rotateVertical, 0, 0);
        }

        /// <summary>
        /// lock mouse to the screen
        /// </summary>
        void LockMouse()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}