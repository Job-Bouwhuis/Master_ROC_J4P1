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

        int _currentLoadedAmmo;
        int currentLoadedAmmo { get { return _currentLoadedAmmo; } set {
                _currentLoadedAmmo = value; 
                onAmmoChanged(value); 
            } }
        int _currentUnloadedAmmo;
        int currentUnloadedAmmo { get { return _currentUnloadedAmmo; } set { _currentUnloadedAmmo = value; onUnloadedAmmoChanged(value); } }

        void OnPlayerShot()
        {
            if (currentLoadedAmmo > 0)
                currentLoadedAmmo--;
        }

        void onPlayerReload()
        {
            if (currentUnloadedAmmo > 0)
            {
                var needed = (magCapacity - currentLoadedAmmo);
                if (needed > currentUnloadedAmmo)
                {
                    currentLoadedAmmo += currentUnloadedAmmo;
                    currentUnloadedAmmo = 0;
                }
                else
                {
                    currentLoadedAmmo += needed;
                    currentUnloadedAmmo -= needed;
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            var pistolObject = FindObjectOfType<Pistol>();
            pistolObject.onPistolShot += OnPlayerShot;
            pistolObject.onPistolReload += onPlayerReload;
            currentLoadedAmmo = magCapacity;
            currentUnloadedAmmo = magCapacity * totalBeginMags;
        }
    }
}