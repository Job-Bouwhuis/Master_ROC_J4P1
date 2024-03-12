//Creator: Luke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ShadowUprising.WeaponBehaviour;

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

        /// <summary>
        /// is called when the pistol shoots when empty
        /// </summary>
        public Action onPistolShootEmtpy = delegate { };

        Animator pistolAnimator;
        string pistolDefaultState;
        bool canShoot = true;
        bool canReload = true;

        private void Start()
        {
            pistolAnimator = GetComponent<Animator>();
            pistolDefaultState = pistolAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            var ammoHandler = FindObjectOfType<AmmoHandler>();
            ammoHandler.onAmmoChanged += SetShootState;
            ammoHandler.onUnloadedAmmoChanged += SetReloadState;

        }

        void SetReloadState(int ammo)
        {
            canReload = ammo > 0;
        }

        void SetShootState(int ammo)
        {
            canShoot = ammo > 0;
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
                if (canReload)
                    onPistolReload.Invoke();
        }

        public void UseItem()
        {
            if (CanExecute())
                if (canShoot)
                {
                    onPistolShot.Invoke();
                }
                else
                {
                    onPistolShootEmtpy.Invoke();
                }
                
        }
    }
}