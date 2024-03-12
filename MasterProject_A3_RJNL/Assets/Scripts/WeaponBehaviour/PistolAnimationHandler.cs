//Creator: Luke
using ShadowUprising.Items.ItemFunctions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the animations of the animator
/// </summary>
public class PistolAnimationHandler : MonoBehaviour
{
    Animator pistolAnimator;
    // Start is called before the first frame update
    void Start()
    {
        var comp = GetComponent<Pistol>();
        comp.onPistolReload += OnPistolReload;
        comp.onPistolShot += OnPistolShot;
        pistolAnimator = GetComponent<Animator>();
    }

    void OnPistolReload()
    {
        pistolAnimator.SetTrigger("Reload");
    }

    void OnPistolShot()
    {
        pistolAnimator.SetTrigger("Shot");
    }

}
