// Creator: Ruben
using System.Collections;
using System.Collections.Generic;
using ShadowUprising.WeaponBehaviour;
using UnityEngine;

public class CameraHitbox : MonoBehaviour, IHitable
{
    [SerializeField] GameObject brokenCameraPrefab;
    private void Start()
    {
        Assign();
    }

    void Assign()
    {

    }

    /// <summary>
    /// Function that gets called whenever this hitbox is hit by the players gun
    /// </summary>
    public void HitEvent()
    {
        Instantiate(brokenCameraPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
