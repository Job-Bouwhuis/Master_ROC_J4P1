// Creator: Ruben
using ShadowUprising.UnityUtils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Detection
{
    public class DetectionManager : Singleton<DetectionManager>
    {
        [Tooltip("The speed at which the detection process occurs in seconds")]
        public float detectionSpeed;
        [Tooltip("List of all objects that are currently detecting the player")]
        public List<GameObject> objectsDetectingPlayer;
        /// <summary>
        /// Event that is called everytime an object gets added to objectsDetectingPlayer
        /// </summary>
        public Action onObjectDetectingPlayer = delegate { };
        /// <summary>
        /// Event that is called when the last object inside objectsDetectingPlayer gets removed from the list
        /// </summary>
        public Action onNoObjectsDetectingPlayer = delegate { };

        /// <summary>
        /// Add an object to the list of objects detecting the player
        /// </summary>
        /// <param name="detectingObject">The object that is currently detecting the player</param>
        public void AddDetecting(GameObject detectingObject)
        {
            objectsDetectingPlayer.Add(detectingObject);
            InvokeOnObjectDetectingPlayer();
        }

        /// <summary>
        /// Remove an object to the list of objects detecting the player
        /// </summary>
        /// <param name="detectingObject">The object that is no longer detecting the player</param>
        public void RemoveDetecting(GameObject detectingObject)
        {
            if (!objectsDetectingPlayer.Contains(detectingObject))
                return;
            objectsDetectingPlayer.Remove(detectingObject);
            if (objectsDetectingPlayer.Count == 0)
                InvokeOnNoObjectsDetectingPlayer();

        }

        void InvokeOnObjectDetectingPlayer()
        {
            onObjectDetectingPlayer.Invoke();
        }

        void InvokeOnNoObjectsDetectingPlayer()
        {
            onNoObjectsDetectingPlayer.Invoke();
        }
    }
}