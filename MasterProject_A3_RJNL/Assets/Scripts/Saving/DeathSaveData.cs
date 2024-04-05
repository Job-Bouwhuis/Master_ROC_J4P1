using ShadowUprising.Items;
using ShadowUprising.WeaponBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.DeathSaves
{
    public class DeathSaveData
    {
        public static DeathSaveData Empty { get; } = new();

        public List<Item> PlayerInventory => playerInventory;
        private List<Item> playerInventory;
        public int ammoLoaded;
        public int ammoInInv;

        public DeathSaveData(List<Item> playerInventory, AmmoHandler ammoHandler)
        {
            this.playerInventory = playerInventory;
            ammoLoaded = ammoHandler.CurrentLoadedAmmo;
            ammoInInv = ammoHandler.CurrentUnloadedAmmo;
        }

        private DeathSaveData()
        {
            playerInventory = new List<Item>();
        }
    }
}