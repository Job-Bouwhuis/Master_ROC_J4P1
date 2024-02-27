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

            masterVolumeSlider.value = settings.masterVolume;
            musicVolumeSlider.value = settings.musicVolume;
            sfxVolumeSlider.value = settings.sfxVolume;
            sensitivitySlider.value = settings.sensitivity;

            useDialogueToggle.toggleState = settings.useVoiceDialogue;
            useSubtitles.toggleState = settings.useSubtitles;
        }

        /// <summary>
        /// Applies the settings to the game.
        /// </summary>
        public void ApplySettings()
        {
            GameSettings settings = GameSettings.Instance;

            settings.masterVolume = masterVolumeSlider.value;
            settings.musicVolume = musicVolumeSlider.value;
            settings.sfxVolume = sfxVolumeSlider.value;
            settings.sensitivity = sensitivitySlider.value;

            settings.useVoiceDialogue = useDialogueToggle.toggleState;
            settings.useSubtitles = useSubtitles.toggleState;
        }

        private void Start()
        {
            SetSavedValuesToInputs();
        }
    }
}