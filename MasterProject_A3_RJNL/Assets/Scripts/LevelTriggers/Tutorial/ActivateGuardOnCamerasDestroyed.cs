using System.Collections;
using System.Collections.Generic;
using ShadowUprising;
using ShadowUprising.SecurityCamera;
using ShadowUprising.UI.InfoDialogs;
using UnityEngine;

namespace ShadowUprising.LevelTriggers.Tutorial
{
    /// <summary>
    /// This component has the singular task to activate a guard when a certain amount of cameras are destroyed
    /// This component is specifically created for a section within the tutorial, although it could easily be reuseds
    /// </summary>
    public class ActivateGuardOnCamerasDestroyed : MonoBehaviour
    {
        [Tooltip("The guard that will be activated when the required amount of cameras are destroyed")]
        [SerializeField] GameObject guard;
        [Tooltip("The amount of cameras that have to be destroyed for guard to spawn")]
        [SerializeField] int toBeDestroyedCameras;
        public int destroyedCamera;
        DestroyedCameraCounter destroyedCameraCounter;

        void Start()
        {
            Assign();
        }

        void Assign()
        {
            destroyedCameraCounter = FindObjectOfType<DestroyedCameraCounter>();
            if (destroyedCameraCounter != null)
                destroyedCameraCounter.onCameraDestroy += OnCameraDestroy;
            else
                Log.PushWarning("No DestroyCameraCounter found in scene. This will stop the SpawnGuardOnCamerasDestroyed trigger from working properly");

        }
        
        void OnCameraDestroy()
        {
            if (++destroyedCamera == toBeDestroyedCameras)
            {
                InfoDialogManager.Instance.ShowInfoDialog("TUTORIAL", "Now shoot the guard that will come!", 5);
                guard.SetActive(true);
            }
        }
    }
}