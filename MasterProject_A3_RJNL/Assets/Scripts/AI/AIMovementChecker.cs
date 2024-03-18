//Creator: Luke
using System;
using UnityEngine;

namespace ShadowUprising.AI
{
    /// <summary>
    /// checks if the ai is moving
    /// </summary>
    public class AIMovementChecker : MonoBehaviour
    {

        /// <summary>
        /// is called when the player is moving
        /// </summary>
        public Action onAIMoving = delegate { };
        /// <summary>
        /// is called when the player is standing
        /// </summary>
        public Action onAIStanding = delegate { };

        Vector3 currentPos;
        Vector3 lastPos;

        // Update is called once per frame
        void Update()
        {
            currentPos = transform.position;
            if (currentPos != lastPos)
            {
                lastPos = currentPos;
                onAIMoving();
            }
            else
                onAIStanding();

        }

    }
}