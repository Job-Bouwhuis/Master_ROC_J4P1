// Creator: Job
using ShadowUprising.UI.Loading;
using ShadowUprising.UI.PauseMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WinterRose;

namespace ShadowUprising.UI.ButtonFunctions
{
    public class LoadScene : ButtonFunction
    {
        public string sceneName;

        public override void Invoke(TextButton button)
        {
            if (LoadingScreen.Instance == null)
            {
                Log.PushError("LoadingScreen is not present in the scene. Please start the game from the main menu.");

#if UNITY_EDITOR
                Windows.DialogResult result = Windows.MessageBox("LoadingScreen is not present in the scene. Would you like to go to the main menu to get it now?", "Error", Windows.MessageBoxButtons.YesNo);

                if (result is Windows.DialogResult.Yes)
                {
                    SceneManager.LoadScene("MainMenu");
                }
#endif
                return;
            }

            LoadingScreen.Instance.ShowAndLoad(sceneName);
        }
    }
}