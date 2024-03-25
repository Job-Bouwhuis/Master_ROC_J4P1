// Creator: Job
using System.Collections;
using UnityEngine;

namespace ShadowUprising.UI.MainMenu
{
    /// <summary>
    /// The model preview, rotates the model and scales it up when entering the main menu.
    /// </summary>
    public class ModelPreview : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The amount of rotations the model will rotate per second")]
        public float rotationSpeed = 0.1f;
        [Tooltip("The speed at which the model will enter the screen after loading to the main menu")]
        public float entrySpeed = 1f;

        [Header("Models")]
        [Tooltip("One of these models can be chosen to be shown on the main menu")]
        public GameObject[] models;

        [Header("Debug - DO NOT CHANGE")]
        [SerializeField] int currentModel = 0;
        [SerializeField] float waitTime = 0;

        // Start is called before the first frame update
        void Start()
        {
            if (Time.realtimeSinceStartup > 20)
                waitTime = 2;

            transform.localScale = Vector3.zero;

            StartCoroutine(EntryAnimationModel());
        }
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * 360 * Time.deltaTime, Space.Self);
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

    }
}