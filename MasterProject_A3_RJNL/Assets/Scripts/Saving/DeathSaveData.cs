using ShadowUprising.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.DeathSaves
{
    public class DeathSaveData
    {
        public static DeathSaveData Empty { get; } = new DeathSaveData();

        public List<Item> PlayerInventory => playerInventory;
        private List<Item> playerInventory;

        public DeathSaveData(List<Item> playerInventory)
        {
            this.playerInventory = playerInventory;
        }

        private DeathSaveData()
        {
            playerInventory = new List<Item>();
        }

        // TODO: Add more data to save when things get merged into the main project.
        // for now, just saving the players inventory to make a start on the saving system.
    }
}