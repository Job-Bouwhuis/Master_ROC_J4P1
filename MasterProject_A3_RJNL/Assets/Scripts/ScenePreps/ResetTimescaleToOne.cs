using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimescaleToOne : MonoBehaviour, IScenePrepOperation
{
    public bool IsComplete { get; set; }

    public void FinishPrep() { }

    public void StartPrep() 
    {
        Time.timeScale = 1;
    }

    public YieldInstruction PrepUpdate()
    {
        return new Completed();
    }
}
