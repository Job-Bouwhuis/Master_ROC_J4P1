// Creator: Job
using ShadowUprising.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ShadowUprising.Settings
{
    /// <summary>
    /// Sets the saved settings to the input fields and applies the settings to the game.
    /// </summary>
    public class SettingsApplyer : MonoBehaviour
    {
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider sensitivitySlider;

        [SerializeField] private TextButton useDialogueToggle;
        [SerializeField] private TextButton useSubtitles;

        /// <summary>
        /// Sets the saved settings to the input fields.
        /// </summary>
        public void SetSavedValuesToInputs()
        {
            GameSettings settings = GameSettings.Instance;

            masterVolumeSlider.value = settings.MasterVolume;
            musicVolumeSlider.value = settings.MusicVolume;
            sfxVolumeSlider.value = settings.SfxVolume;
            sensitivitySlider.value = settings.Sensitivity;

            useDialogueToggle.toggleState = settings.UseVoiceDialogue;
            useSubtitles.toggleState = settings.UseSubtitles;
        }

        /// <summary>
        /// Applies the settings to the game.
        /// </summary>
        public void ApplySettings() => GameSettings.Instance
            .UpdateSettingsBulk(masterVolumeSlider.value,
                                musicVolumeSlider.value,
                                sfxVolumeSlider.value,
                                sensitivitySlider.value,

                                useDialogueToggle.toggleState,
                                useSubtitles.toggleState);

        private void Start()
        {
            SetSavedValuesToInputs();
        }
    }
}