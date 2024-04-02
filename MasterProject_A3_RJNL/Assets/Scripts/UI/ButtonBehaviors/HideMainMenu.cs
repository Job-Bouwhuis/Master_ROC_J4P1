// Creator: Job
using ShadowUprising.UI.Loading;
using ShadowUprising.UI.MainMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    /// <summary>
    /// Button function that hides the main menu
    /// </summary>
    public class HideMainMenu : ButtonFunction
    {
        [SerializeField] private MainMenuController mainMenuController;

        public override void Invoke(TextButton button)
        {
            mainMenuController.state = MainMenuController.MenuState.None;
        }
    }
}