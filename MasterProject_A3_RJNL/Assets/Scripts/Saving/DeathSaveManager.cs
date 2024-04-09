using ShadowUprising.GameOver;
using ShadowUprising.Inventory;
using ShadowUprising.Items;
using ShadowUprising.Player;
using ShadowUprising.UI.Loading;
using ShadowUprising.UnityUtils;
using ShadowUprising.WeaponBehaviour;
using System.Collections.Generic;
using UnityEngine;
using WinterRose;

namespace ShadowUprising.DeathSaves
{
    [DontDestroyOnLoad]
    public class DeathSaveManager : Singleton<DeathSaveManager>, IScenePrepOperation
    {
        public DeathSaveData dataSnapshot;

        public bool IsComplete { get; set; }
        public bool IsResetting { get; set; }

        protected override void Awake()
        {
            base.Awake();

            if (InventoryManager.Instance == null)
            {
                Windows.MessageBox("No inventory in the scene. Please add one for gameplay sake.");
                return;
            }

            if (FindAnyObjectByType<AmmoHandler>() == null)
            {
                Windows.MessageBox("No AmmoHandler in the scene. Please add one for gameplay sake.");
                return;
            }

            dataSnapshot = DeathSaveData.Empty;

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoad;
        }

        /// <summary>
        /// Makes a snapshot of the current state of the game.
        /// </summary>
        public void MakeSnapshot()
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name is "MainMenu" or "DEMOEND")
            {
                Log.Push("Not loading snapshot in main menu or demo end.\n" +
                    "Due to being in either of these scenes, resetting the game save.");
                dataSnapshot = DeathSaveData.Empty;
                return;
            }
            AmmoHandler ammoHandler = FindAnyObjectByType<AmmoHandler>();
            PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();
            dataSnapshot = new DeathSaveData(
                new List<Item>(InventoryManager.Instance.playerInventory),
                ammoHandler,
                playerStats.health);

            Log.Push("Game snapshot made.");
        }

        /// <summary>
        /// Loads the previously recorded snapshot.
        /// </summary>
        public void LoadSnapshot()
        {
            IsResetting = false;
            GameOverManager.Instance.UnGameOver();
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name is "MainMenu" or "DEMOEND")
            {
                Log.Push("Not loading snapshot in main menu or demo end.\n" +
                    "Due to being in either of these scenes, resetting the game save.");
                dataSnapshot = DeathSaveData.Empty;
                Destroy(gameObject);
                Instance = null;
                return;
            }
            if (dataSnapshot is null)
            {
                Debug.Log("No snapshot made. Cant load. ");
                return;
            }

            InventoryManager.Instance.ClearInventory();

            foreach (var item in dataSnapshot.PlayerInventory)
            {
                item.ResetFunction();
                InventoryManager.Instance.AddItem(item);
            }

            AmmoHandler ammoHandler = FindAnyObjectByType<AmmoHandler>();
            ammoHandler.CurrentLoadedAmmo = dataSnapshot.ammoLoaded;
            ammoHandler.CurrentUnloadedAmmo = dataSnapshot.ammoInInv;

            PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();
            playerStats.health = dataSnapshot.health;

            Log.Push("Game snapshot loaded.");
        }

        public void StartPrep() { }
        public YieldInstruction PrepUpdate()
        {
            if (IsResetting)
                LoadSnapshot();
            else
                MakeSnapshot();
            return new Completed();
        }
        public void FinishPrep() { }

        private void OnSceneLoad(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            if (gameObject.IsDestroyed())
                return;

            string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (currentScene is "MainMenu" or "DEMOEND")
            {
                Instance = null;
                Destroy(gameObject);
                UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoad;
            }
        }
    }
}