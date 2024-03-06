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
        /// <summary>
        /// is called when the pistol shoots
        /// </summary>
        public Action onPistolShot = delegate { };
        /// <summary>
        /// is called when the pistol reloads
        /// </summary>
        public Action onPistolReload = delegate { };

        Animator pistolAnimator;
        string pistolDefaultState;
        bool allowed;

        private void Start()
        {
            pistolAnimator = GetComponent<Animator>();
            pistolDefaultState = pistolAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }

        private void Update()
        {
            if (CanExecute())
                ExecuteGunFunctions();
        }

        private bool CanExecute()
        {
            return pistolAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == pistolDefaultState;
        } 

        public void ExecuteGunFunctions()
        {
            if (Input.GetKeyUp(KeyCode.R))
                onPistolReload.Invoke();
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {

                UseItem();
            }

            if (Input.GetKeyUp(KeyCode.R))
                onPistolReload.Invoke();
            allowed = false;
        }

        public void UseItem()
        {
            if (CanExecute())
                onPistolShot.Invoke();
        }
    }
}