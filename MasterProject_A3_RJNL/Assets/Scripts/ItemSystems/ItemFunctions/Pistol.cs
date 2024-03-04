//Creator: Luke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ShadowUprising.Items.ItemFunctions
{

    /// <summary>
    /// represents the functionality of the pistol
    /// </summary>
    public class Pistol : MonoBehaviour, IItemFunction
    {

        public Action onPistolShot = delegate { };
        public Action onPistolReload;


        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.R))
                onPistolReload.Invoke();
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {

                UseItem();
            }
        }

        public void UseItem()
        {
            onPistolShot.Invoke();
        }
    }
}