using ShadowUprising;
using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMouseInMainMenu : MonoBehaviour, IScenePrepOperation
{
    public bool IsComplete { get; set; }

    public void FinishPrep() { }

    public void StartPrep()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Log.Push("Mouse unlocked in Main Menu");
    }

    public YieldInstruction Update()
    {
        return new Completed();
    }
}
