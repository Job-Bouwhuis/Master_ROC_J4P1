using ShadowUprising.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    public class SettingsSaveAndReturn : ButtonFunction
    {
        [SerializeField] private SettingsApplyer settingsApplyer;
        [SerializeField] private MainMenuController mainMenuController;

        public override void Invoke(TextButton button, bool isToggle)
        {
            settingsApplyer.ApplySettings();
            GameSettings.Instance.Save();

            mainMenuController.state = MainMenuController.MenuState.Main;
        }
    }
}
