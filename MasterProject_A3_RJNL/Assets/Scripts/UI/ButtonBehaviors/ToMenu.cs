using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    public class ToMenu : ButtonFunction
    {
        [SerializeField] private MainMenuController mainMenuController;
        [SerializeField] private MainMenuController.MenuState menuState;

        public override void Invoke(TextButton button, bool isToggle) => mainMenuController.state = menuState;
    }
}