using ShadowUprising.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShadowUprising.Settings
{
    public class SettingsApplyer : MonoBehaviour
    {
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider sensitivitySlider;

        [SerializeField] private TextButton useDialogueToggle;
        [SerializeField] private TextButton useSubtitles;

        private void Start()
        {
            GameSettings settings = GameSettings.Instance;

            masterVolumeSlider.value = settings.masterVolume;
            musicVolumeSlider.value = settings.musicVolume;
            sfxVolumeSlider.value = settings.sfxVolume;

            useDialogueToggle.toggleState = settings.useVoiceDialogue;
            useSubtitles.toggleState = settings.useSubtitles;
        }

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
    }
}