//Creator: Luke
using UnityEngine;
using System;


namespace ShadowUprising.NighVision
{
    /// <summary>
    /// handles everything for the nightvision
    /// </summary>
    public class NighVisionHandler : MonoBehaviour
    {
        /// <summary>
        /// refrence to the postprocessing volume
        /// </summary>
        public GameObject volumeObject;

        /// <summary>
        /// is called when the nightivision is enabled
        /// </summary>
        public Action onNightvisionEnabled = delegate { };

        /// <summary>
        /// is called when the nightvision is disabled
        /// </summary>
        public Action onNightvisionDisabled = delegate { };

        /// <summary>
        /// determines if the item is pickedUp
        /// </summary>
        public bool pickedUp;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.V) && pickedUp)
                EnableOrDisableVision();
        }

        void EnableOrDisableVision()
        {
            volumeObject.SetActive(!volumeObject.active);

            if (volumeObject.active)
                onNightvisionEnabled();
            else
                onNightvisionDisabled();
        }

    }
}