// Creator: Job
using ShadowUprising.UI.MainMenu;
using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    /// <summary>
    /// Button function to change the menu state of the main menu controller
    /// </summary>
    public class ToMenu : ButtonFunction
    {
        [SerializeField] private MainMenuController mainMenuController;
        [SerializeField] private MainMenuController.MenuState menuState;

        public override void Invoke(TextButton button) => mainMenuController.state = menuState;
    }
}