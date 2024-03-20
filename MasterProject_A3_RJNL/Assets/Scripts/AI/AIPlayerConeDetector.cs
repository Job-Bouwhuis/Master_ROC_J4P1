//Creator: Luke
//Edited: Ruben
using System;
using System.Collections.Generic;
using UnityEngine;
using ShadowUprising.Detection;

namespace ShadowUprising.AI
{
    /// <summary>
    /// checks if the player is within the detection cone and if so it calls an event
    /// </summary>
    public class AIPlayerConeDetector : MonoBehaviour
    {

        /// <summary>
        /// the max range the dectector is able to spot the player from
        /// </summary>
        public int range;

        /// <summary>
        /// the max angle the detector is able to view
        /// </summary>
        public int coneAngle;

        /// <summary>
        /// Is called when an object is detected and returns the Object that has been detected
        /// </summary>
        public Action<GameObject> onObjectDetected = delegate { };
        /// <summary>
        /// Is called when the enemy goes through its detection cycle and finds nothing
        /// </summary>
        public Action onNothingDetected = delegate { };
        DetectableObjects detectableObjects;

        private void Start()
        {
            Asign();
        }

        void Asign()
        {
            detectableObjects = FindAnyObjectByType<DetectableObjects>();
            if (detectableObjects == null) 
                Log.PushWarning("No DetectableObjects component in scene. Without this enemies will not detect players and other detectableObjects. This component should be in the Object 'DetectionManager'. This object should have a prefab");
        }

        // Update is called once per frame
        void Update()
        {
            UpdateDetection();
        }

        void UpdateDetection()
        {
            if (detectableObjects == null)
                return;
            GameObject detectedObject = Detect(detectableObjects.targets);
            if (detectedObject != null)
                OnObjectDetected(detectedObject);
            else
                OnNothingDetected();
        }

        GameObject Detect(List<GameObject> targets)
        {
            foreach (GameObject target in targets)
            {
                var heading = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(heading, transform.forward) < coneAngle)
                {
                    Physics.Raycast(new Ray(transform.position, heading), out RaycastHit hitinfo, range);
                    if (hitinfo.transform.gameObject == target)
                        return hitinfo.transform.gameObject;
                }
            }
            return null;
        }

        void OnObjectDetected(GameObject detectedObject)
        {
            onObjectDetected.Invoke(detectedObject);
        }

        void OnNothingDetected()
        {
            onNothingDetected.Invoke();
        }
    }
}