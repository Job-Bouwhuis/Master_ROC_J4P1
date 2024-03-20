//Creator: Luke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowUprising.Items.ItemFunctions;
using System;

namespace ShadowUprising.WeaponBehaviour
{
    /// <summary>
    /// handles the raycast of the pistol
    /// </summary>
    public class PistolRaycastHandler : MonoBehaviour
    {
        /// <summary>
        /// is called when the pistol hits something
        /// </summary>
        public Action<RaycastHit> onRaycastHit = delegate { };

        Transform camTransform;
        [SerializeField] float hitDistance;
        

        // Start is called before the first frame update
        void Start()
        {
            Assign();
        }

        void Assign()
        {
            GetComponent<Pistol>().onPistolShot += OnPistolShot;
            camTransform = Camera.main.transform;
        }

        void OnPistolShot()
        {
            if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hitinfo, hitDistance))
            {
                onRaycastHit.Invoke(hitinfo);
            }
        }
    }
}