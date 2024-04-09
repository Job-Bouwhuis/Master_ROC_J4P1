using UnityEngine;

namespace ShadowUprising.Tutorial
{
    /// <summary>
    /// Component responsible for making the player look at a certain object during tutorial voice lines.
    /// </summary>
    public class LookAt : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform cameraTransform; // for rotating vertically
        [SerializeField] private Transform player; // for rotating horizontally
        [Tooltip("When true, this component will force the player to look at its target.")]
        public bool doLookAt = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                doLookAt = true;
            }
        }

        void Update()
        {
            if (doLookAt)
            {
                // Rotate camera vertically
                Vector3 targetDirection = target.position - cameraTransform.position;
                cameraTransform.rotation = Quaternion.LookRotation(targetDirection);

                // Rotate player horizontally
                Vector3 horizontalDirection = target.position - player.position;
                horizontalDirection.y = 0f;
                player.rotation = Quaternion.LookRotation(horizontalDirection);
            }
        }
    }
}
