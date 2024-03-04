using ShadowUprising.Items.ItemFunctions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    void OnPistolShot()
    {
        pistolAnimator.SetTrigger("Shot");
    }

}
