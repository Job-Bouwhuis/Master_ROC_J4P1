using UnityEngine;
using System.Collections;

namespace ShadowUprising.UI.MainMenu
{
    /// <summary>
    /// The model preview, rotates the model and scales it up when entering the main menu.
    /// Allows the user to rotate the model by dragging with the mouse.
    /// </summary>
    public class ModelPreview : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The amount of rotations the model will rotate per second")]
        public float constantRotationSpeed = 0.1f; // Constant rotation speed
        [Tooltip("The speed at which the model will enter the screen after loading to the main menu")]
        public float entrySpeed = 1f;
        [Tooltip("The speed of rotation when dragging with the mouse")]
        public float dragRotationSpeed = 1f;
        [Tooltip("The speed at which the rotation returns to constant speed after dragging")]
        public float returnToConstantSpeed = 2f; // Speed of return to constant rotation speed
        [Tooltip("The time it takes for the model to return to its original orientation after dragging")]
        public float returnToOriginalTime = 1f; // Time to return to original orientation

        [Header("Models")]
        [Tooltip("One of these models can be chosen to be shown on the main menu")]
        public GameObject[] models;

        [Header("Debug - DO NOT CHANGE")]
        [SerializeField] int currentModel = 0;
        [SerializeField] float waitTime = 0;
        private Quaternion originalRotation;
        private Vector3 originalEulerAngles;
        private Vector3 lastMousePosition;
        private Vector3 lastRotation;
        private bool isDragging = false;
        private bool hasDragged = false;
        private bool isReturning = false;
        private float currentRotationSpeed; // Current rotation speed

        // Start is called before the first frame update
        void Start()
        {
            if (Time.realtimeSinceStartup > 20)
                waitTime = 2;

            transform.localScale = Vector3.zero;
            originalRotation = transform.rotation;
            originalEulerAngles = transform.eulerAngles;

            StartCoroutine(EntryAnimationModel());
            currentRotationSpeed = constantRotationSpeed; // Initialize current rotation speed
        }

        // Update is called once per frame
        void Update()
        {
            // If not dragging or hasDragged, apply constant rotation speed in local space
            if (!isDragging && !hasDragged && !isReturning)
                transform.Rotate(Vector3.up, currentRotationSpeed * 360 * Time.deltaTime, Space.Self);

            // If dragging with mouse, rotate model accordingly in world space
            if (isDragging)
            {
                Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
                float deltaX = -mouseDelta.x * dragRotationSpeed; // Inverted x-axis for dragging
                float deltaY = mouseDelta.y * dragRotationSpeed; // Inverted y-axis for dragging
                transform.Rotate(Vector3.up, deltaX, Space.World);
                transform.Rotate(Vector3.right, deltaY, Space.World);
            }

            // Save current mouse position and rotation for the next frame
            lastMousePosition = Input.mousePosition;
            lastRotation = transform.rotation.eulerAngles;

            // Smoothly transition back to constant rotation speed after releasing mouse button
            if ((!Input.GetMouseButton(0) || isReturning) && !isDragging && currentRotationSpeed != constantRotationSpeed)
            {
                currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, constantRotationSpeed, returnToConstantSpeed * Time.deltaTime);
            }

            // Continue rotating based on drag velocity after releasing mouse button
            if (!Input.GetMouseButton(0) && !isDragging && hasDragged)
            {
                hasDragged = false;
                isReturning = true;
                StartCoroutine(ReturnToOriginalRotation());
            }
        }

        IEnumerator EntryAnimationModel()
        {
            if (waitTime != 0)
                yield return new WaitForSeconds(waitTime);

            while (transform.localScale.x < 0.99f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, entrySpeed * Time.deltaTime);
                yield return null;
            }

            transform.localScale = Vector3.one;
        }

        void OnMouseDown()
        {
            if (!isReturning)
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition;
                lastRotation = transform.rotation.eulerAngles;
                currentRotationSpeed = 0f; // Stop constant rotation while dragging
            }
        }

        void OnMouseUp()
        {
            if (!isReturning)
            {
                isDragging = false;
                hasDragged = true;
            }
        }

        IEnumerator ReturnToOriginalRotation()
        {
            Quaternion startRotation = Quaternion.Euler(lastRotation);
            Quaternion targetRotation = originalRotation;
            float elapsedTime = 0f;

            while (elapsedTime < returnToOriginalTime)
            {
                float t = elapsedTime / returnToOriginalTime;
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = targetRotation;
            isReturning = false;

            // Restart constant rotation after returning to original orientation
            currentRotationSpeed = constantRotationSpeed;
        }
    }
}
