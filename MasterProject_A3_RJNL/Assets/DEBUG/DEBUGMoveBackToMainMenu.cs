using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.DEBUG
{
    public class DEBUGMoveBackToMainMenu : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                LoadingScreen.Instance.ShowAndLoad("MainMenu");
            }
        }
    }
}