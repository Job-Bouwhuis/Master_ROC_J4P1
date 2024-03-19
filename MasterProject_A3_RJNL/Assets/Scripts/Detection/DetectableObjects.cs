// Creator: Ruben
using ShadowUprising;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Detection
{
    /// <summary>
    /// This script manages all the objects in the scene that can be detected by the either the Guards or the Security Cameras.
    /// This component is intended to be put on the DetectionManager gameobject (could be imported in scene via prefab)
    /// </summary>
    public class DetectableObjects : MonoBehaviour
    {
        public List<GameObject> targets = new List<GameObject>();

        void Start()
        {
            Assign();
        }

        void Assign()
        {
            // Very unoptimised line. To be optimised if the time is found
            GameObject playerTransform = GameObject.FindWithTag("Player");
            if (playerTransform != null)
                targets.Add(playerTransform);
            else
                Log.PushError("Cannot add player to DetectableObjects because no player can be found in scene.");

        }

        public void AddDetectableObject(GameObject detectableObject)
        {
            targets.Add(detectableObject);
        }

        public void RemoveDetectableObject(GameObject detectableObject)
        {
            if (targets.Contains(detectableObject))
                targets.Remove(detectableObject);
        }
    }
}