// Creator: Ruben
// Edited by:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // Refrences
        [Header("References")]
        private Rigidbody rb;
        // Variables

        const int DISTANCE_TO_WALL = 1;
        [SerializeField] private int baseSpeed;
        [SerializeField] private int movementSpeedModifier;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            Vector3 moveDir = Vector3.zero;
            moveDir += transform.forward * Input.GetAxisRaw("Vertical");
            moveDir += transform.right * Input.GetAxisRaw("Horizontal");
            moveDir = Vector3.Normalize(moveDir);

            if (moveDir != Vector3.zero && !CheckIfMovementBlocked(moveDir))
                rb.AddForce(moveDir * (baseSpeed + movementSpeedModifier));
        }

        bool CheckIfMovementBlocked(Vector3 moveDir)
        {
            Ray ray = new Ray(transform.position, moveDir);
            Physics.Raycast(ray, out RaycastHit hitData);
            if (hitData.distance <= DISTANCE_TO_WALL && hitData.distance != 0)
                return true;
            return false;
        }

        public void UpdateMovementSpeedModifier(int modifier)
        {
            movementSpeedModifier = modifier;
        }

        public void ResetMovementSpeedModifier()
        {
            movementSpeedModifier = 0;
        }
    }
}
