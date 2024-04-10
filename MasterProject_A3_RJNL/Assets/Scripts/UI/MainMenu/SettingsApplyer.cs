// Creator: Job
// Edited by: Luke
using ShadowUprising.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

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

        /// <summary>
        /// holder for the audiomixer within the game
        /// </summary>
        public AudioMixer mixer;
        public AudioMixerGroup watthefuck;


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
            SetAudioValuesToInGameAudio();
        }

        private float SliderValueToDB(float value)
        {
            var convertedSliderValue = 1 - value;
            return -(convertedSliderValue * 60);
        } 

        private void SetAudioValuesToInGameAudio()
        {
            mixer.SetFloat("Master", SliderValueToDB(masterVolumeSlider.value));
            mixer.SetFloat("Music", SliderValueToDB(musicVolumeSlider.value));
            mixer.SetFloat("SFX", SliderValueToDB(sfxVolumeSlider.value));
        }

        /// <summary>
        /// Applies the settings to the game.
        /// </summary>
        public void ApplySettings()
        {
            GameSettings.Instance
.UpdateSettingsBulk(masterVolumeSlider.value,
      musicVolumeSlider.value,
      sfxVolumeSlider.value,
      sensitivitySlider.value,
      useDialogueToggle.toggleState);
            SetAudioValuesToInGameAudio();
        }
        private void Start()
        {
            SetSavedValuesToInputs();
        }
    }
}