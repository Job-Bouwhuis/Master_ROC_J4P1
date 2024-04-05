// Creator: Ruben
using System.Collections;
using System.Collections.Generic;
using ShadowUprising;
using ShadowUprising.SecurityCamera;
using ShadowUprising.WeaponBehaviour;
using Unity.VisualScripting;
using UnityEngine;

public class CameraHitbox : MonoBehaviour, IHitable
{
    [SerializeField] GameObject brokenCameraPrefab;
    DestroyedCameraCounter destroyedCameraCounter;

    private void Start()
    {
        Assign();
    }

    void Assign()
    {
        destroyedCameraCounter = FindObjectOfType<DestroyedCameraCounter>();
        if (destroyedCameraCounter == null)
            Log.PushWarning("This scene does not contain a DestroyedCameraCounter component. this means destroyed cameras will not be counter");
    }

    /// <summary>
    /// Function that gets called whenever this hitbox is hit by the players gun
    /// </summary>
    public void HitEvent()
    {
        if (destroyedCameraCounter.IsDestroyed())
            return;
        if (destroyedCameraCounter != null)
            destroyedCameraCounter.addDestroyedCamera();
        CameraPlayerDetectionEvent cameraPlayerDetectionEvent = GetComponent<CameraPlayerDetectionEvent>();
        if (cameraPlayerDetectionEvent != null)
            cameraPlayerDetectionEvent.OnNothingDetected();
        Instantiate(brokenCameraPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
