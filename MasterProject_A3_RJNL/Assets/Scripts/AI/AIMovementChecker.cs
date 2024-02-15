using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{



    public class AIMovementChecker : MonoBehaviour
    {
        Vector3 currentPos;
        Vector3 lastPos;

        public Action onAIMoving = delegate { };
        public Action onAIStanding = delegate { };

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