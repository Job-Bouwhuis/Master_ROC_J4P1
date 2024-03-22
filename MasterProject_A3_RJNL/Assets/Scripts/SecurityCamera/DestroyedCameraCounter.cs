// Creator: Ruben
using System;
using System.Collections.Generic;
using ShadowUprising.GameOver;
using UnityEngine;

namespace ShadowUprising.SecurityCamera
{
    public class DestroyedCameraCounter : MonoBehaviour
    {
        int destroyedCameraCount;
        [SerializeField] int maxDestroyedCameras;

        public Action onCameraDestroy;

        public void addDestroyedCamera()
        {
            onCameraDestroy.Invoke();
            if (destroyedCameraCount++ > maxDestroyedCameras && GameOverManager.Instance != null)
                GameOverManager.Instance.GameOver();
        }
    }
}