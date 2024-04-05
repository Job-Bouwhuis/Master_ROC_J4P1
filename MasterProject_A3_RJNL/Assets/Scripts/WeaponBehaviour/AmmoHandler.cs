// Creator: Luke
// Edited: Ruben
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ShadowUprising.Items.ItemFunctions;

namespace ShadowUprising.WeaponBehaviour
{
    /// <summary>
    /// handles the ammo of the player
    /// </summary>
    public class AmmoHandler : MonoBehaviour
    {
        public Action<int> onAmmoChanged = delegate { };
        public Action<int> onUnloadedAmmoChanged = delegate { };
        public int magCapacity;
        public int totalBeginMags;
        [SerializeField] Pistol pistolObject;

        int _currentLoadedAmmo;
        
        /// <summary>
        /// How much ammo is currently loaded in the gun
        /// </summary>
        public int CurrentLoadedAmmo 
        { 
            get => _currentLoadedAmmo; 
            set
            {
                _currentLoadedAmmo = value; 
                onAmmoChanged(value); 
            } 
        }
        int _currentUnloadedAmmo;

        /// <summary>
        /// How much ammo is left in the players inventory
        /// </summary>
        public int CurrentUnloadedAmmo 
        { 
            get => _currentUnloadedAmmo;
            set 
            { 
                _currentUnloadedAmmo = value; 
                onUnloadedAmmoChanged(value); 
            } 
        }

        void OnPlayerShot()
        {
            if (CurrentLoadedAmmo > 0)
                CurrentLoadedAmmo--;
        }

        void onPlayerReload()
        {
            if (CurrentUnloadedAmmo > 0)
            {
                var needed = (magCapacity - CurrentLoadedAmmo);
                if (needed > CurrentUnloadedAmmo)
                {
                    CurrentLoadedAmmo += CurrentUnloadedAmmo;
                    CurrentUnloadedAmmo = 0;
                }
                else
                {
                    CurrentLoadedAmmo += needed;
                    CurrentUnloadedAmmo -= needed;
                }
            }
        }

        void Start()
        {
            pistolObject.onPistolShot += OnPlayerShot;
            pistolObject.onPistolReload += onPlayerReload;
            if (totalBeginMags == 0)
                CurrentLoadedAmmo = 0;
            else
                CurrentLoadedAmmo = magCapacity;
            CurrentUnloadedAmmo = magCapacity * totalBeginMags;
        }

        /// <summary>
        /// Return the currentlyLoadedAmmo variable
        /// </summary>
        /// <returns></returns>
        public int GetCurrentLoadedAmmo()
        {
            return CurrentLoadedAmmo;
        }

        public void AddAmmoMags(int mags)
        {
            CurrentUnloadedAmmo += mags * magCapacity;
        }
    }
}