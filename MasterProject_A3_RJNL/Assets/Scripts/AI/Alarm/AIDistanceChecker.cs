using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI.Alarm
{
    public class AIDistanceChecker : MonoBehaviour
    {
        public float activationRange;
        public Action<GameObject> onAIWithinRange = delegate { };

        List<GameObject> boundedAI = new List<GameObject>();

        public void AddAI(GameObject gameObject)
        {
            boundedAI.Add(gameObject);
        }

        public void CheckDistanceToObjects()
        {
            foreach (var item in boundedAI)
            {
                if (item != null)
                    if (Vector3.Distance(transform.position, item.transform.position) < activationRange)
                    {
                        onAIWithinRange(item);
                    }
            }
        }

        // Update is called once per frame
        void Update()
        {
            CheckDistanceToObjects();
        }
    }
}