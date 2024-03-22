// Creator: job

using ShadowUprising.Settings;
using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    /// <summary>
    /// Button function to reset the options screen to the saved values
    /// </summary>
    public class ResetOptionScreenToSavedValues : ButtonFunction
    {
        [SerializeField] private SettingsApplyer settingsApplyer;

        public override void Invoke(TextButton button)
        {
            settingsApplyer.SetSavedValuesToInputs();
        }
    }
}