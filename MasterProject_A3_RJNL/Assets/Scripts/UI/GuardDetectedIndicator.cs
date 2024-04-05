using ShadowUprising.AI.Alarm;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDetectedIndicator : MonoBehaviour
{
    AIDecisionHandler[] guards;


    // Start is called before the first frame update
    void Start()
    {
        guards = FindObjectsOfType<AIDecisionHandler>();
        foreach (AIDecisionHandler guard in guards)
        {
            guard.onBodySpotted += OnBodySpotted;
        }
        
    }

    private void OnBodySpotted()
    {
       //TODO: show indicator.
    }
}
