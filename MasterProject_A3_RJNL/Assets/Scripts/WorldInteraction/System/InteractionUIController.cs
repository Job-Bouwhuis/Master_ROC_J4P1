using ShadowUprising.WorldInteraction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUIController : MonoBehaviour
{
    [SerializeField] WorldInteractor worldInteractor; 

    GameObject textObject;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        textObject = transform.GetChild(0).gameObject;
        image = GetComponent<Image>();

        worldInteractor.OnItemLookedAt += ShowUI;
        worldInteractor.OnItemLookedAtStopped += HideUI;
    }

    private void HideUI()
    {
        textObject.SetActive(false);
        image.enabled = false;
    }

    private void ShowUI()
    {
        textObject.SetActive(true);
        image.enabled = true;
    }
}
