// Creator: Ruben
// Edited by: Job
using ShadowUprising.Settings;
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

        [Tooltip("Whether or not the script will use the games sensitivity setting found in PlayerPrefs")]
        public bool useSensitivity = true;

        private void Awake()
        {
            if (useSensitivity)
                horizontalSensitivity = verticalSensitivity = GameSettings.Instance.Sensitivity.FloorToInt();
            LockMouse();

            GameSettings.Instance.OnSettingsChanged += b => 
            {
                if (useSensitivity)
                    horizontalSensitivity = verticalSensitivity = GameSettings.Instance.Sensitivity.FloorToInt();
            };
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