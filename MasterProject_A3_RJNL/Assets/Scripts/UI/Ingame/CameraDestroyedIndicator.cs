using ShadowUprising.SecurityCamera;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShadowUprising.UI.InGame
{
    /// <summary>
    /// Indicator that shows the amount of destroyed cameras
    /// </summary>
    public class CameraDestroyedIndicator : MonoBehaviour
    {
        DestroyedCameraCounter counter;
        ElementAnimator elementAnimator;
        [SerializeField] TMP_Text text;

        // Start is called before the first frame update
        void Awake()
        {
            counter = FindObjectOfType<DestroyedCameraCounter>();
            counter.onCameraDestroy += CameraDestroyed;

            elementAnimator = GetComponent<ElementAnimator>();
            text = GetComponentInChildren<TMP_Text>();

            CameraDestroyed();
        }

        private void CameraDestroyed()
        {
            text.text = counter.destroyedCameraCount + "/" + counter.maxDestroyedCameras;
            elementAnimator.Show();
        }
    }
}