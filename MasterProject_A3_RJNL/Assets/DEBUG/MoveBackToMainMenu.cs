using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackToMainMenu : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            LoadingScreen.Instance.ShowAndLoad("MainMenu");
        }
    }
}
