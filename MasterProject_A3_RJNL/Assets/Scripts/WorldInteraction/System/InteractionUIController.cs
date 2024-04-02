using ShadowUprising.Player;
using ShadowUprising.WorldInteraction;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUIController : MonoBehaviour
{
    public const string NORMAL_TEXT = "Interact (F)";
    public const string DROP_BODY_TEXT = "Drop body (X)";

    [SerializeField] WorldInteractor worldInteractor;
    [SerializeField, Tooltip("Optional parameter. Seeks if the player is holding a dead guard, and if so, displays a message on the HUD how to drop the body")]
    GuardHolder guardHolder;

    GameObject textObject;
    Image image;
    TMP_Text text;
    bool isGuardHeld;

    // Start is called before the first frame update
    void Start()
    {
        textObject = transform.GetChild(0).gameObject;
        image = GetComponent<Image>();

        worldInteractor.OnItemLookedAt += ShowUI;
        worldInteractor.OnItemLookedAtStopped += HideUI;
        text = GetComponentInChildren<TMP_Text>();
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

    private void Update()
    {
        if (guardHolder != null)
        {
            if (guardHolder.HasGuard)
            {
                textObject.SetActive(true);
                text.text = DROP_BODY_TEXT;
                isGuardHeld = true;
            }
            else
            {
                text.text = NORMAL_TEXT;
                if(isGuardHeld)
                    textObject.SetActive(false);
                isGuardHeld = false;
            }
        }
    }
}
