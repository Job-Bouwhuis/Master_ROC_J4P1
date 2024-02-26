// Creator: Ruben
// Edited by:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Refrences")]
        private Rigidbody rb;

        const int DISTANCE_TO_WALL = 1;
        
        [Header("Variables")]
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

        /// <summary>
        /// Add force to the player depending on the inputs of the player
        /// </summary>
        private void UpdateMovement()
        {
            Vector3 moveDir = Vector3.zero;
            moveDir += transform.forward * Input.GetAxisRaw("Vertical");
            moveDir += transform.right * Input.GetAxisRaw("Horizontal");
            moveDir = Vector3.Normalize(moveDir);

            if (moveDir != Vector3.zero && !CheckIfMovementBlocked(moveDir))
                rb.AddForce(moveDir * (baseSpeed + movementSpeedModifier));
        }

        /// <summary>
        /// Check if the Player is going to collide with an object if they head the direction of "moveDir"
        /// </summary>
        /// <param name="moveDir">The direction the player will move in</param>
        /// <returns></returns>
        bool CheckIfMovementBlocked(Vector3 moveDir)
        {
            Ray ray = new Ray(transform.position, moveDir);
            Physics.Raycast(ray, out RaycastHit hitData);
            if (hitData.distance <= DISTANCE_TO_WALL && hitData.distance != 0)
                return true;
            else return false;
        }
    }
}
