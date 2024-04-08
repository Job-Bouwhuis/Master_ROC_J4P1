// Creator: Ruben
// Edited: job
using System;
using ShadowUprising.GameOver;
using UnityEngine;

namespace ShadowUprising.SecurityCamera
{
    public class DestroyedCameraCounter : MonoBehaviour
    {
        /// <summary>
        /// The amount of cameras that have been destroyed 
        /// </summary>
        public int destroyedCameraCount;
        /// <summary>
        /// The maximum amount of cameras that can be destroyed before the game ends
        /// </summary>
        public int maxDestroyedCameras;

        public Action onCameraDestroy;

        public void addDestroyedCamera()
        {
            destroyedCameraCount++;
            onCameraDestroy.Invoke();
            if(destroyedCameraCount > maxDestroyedCameras && GameOverManager.Instance != null)
                GameOverManager.Instance.GameOver("Too many cameras were destroyed!");
        }
    }
}