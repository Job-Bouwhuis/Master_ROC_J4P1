// Creator: Job
using ShadowUprising.Settings;
using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    /// <summary>
    /// Button function that saves the settings and returns to the main menu to the start screen.
    /// </summary>
    public class SaveSettings : ButtonFunction
    {
        [SerializeField] private SettingsApplyer settingsApplyer;

        public override void Invoke(TextButton button)
        {
            settingsApplyer.ApplySettings();
            GameSettings.Instance.Save();
        }
    }
}
