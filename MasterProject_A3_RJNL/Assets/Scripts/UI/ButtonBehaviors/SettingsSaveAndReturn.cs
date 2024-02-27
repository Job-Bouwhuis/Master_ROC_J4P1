// Creator: Job
using ShadowUprising.Settings;
using ShadowUprising.UI.MainMenu;
using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    /// <summary>
    /// Button function that saves the settings and returns to the main menu to the start screen.
    /// </summary>
    public class SettingsSaveAndReturn : ButtonFunction
    {
        [SerializeField] private SettingsApplyer settingsApplyer;
        [SerializeField] private MainMenuController mainMenuController;

        public override void Invoke(TextButton button)
        {
            settingsApplyer.ApplySettings();
            GameSettings.Instance.Save();

            mainMenuController.state = MainMenuController.MenuState.Main;
        }
    }
}
