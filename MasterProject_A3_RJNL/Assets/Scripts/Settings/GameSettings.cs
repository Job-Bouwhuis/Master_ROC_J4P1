// Creator: job
using UnityEngine;
using WinterRose.Serialization;

namespace ShadowUprising.Settings
{
    public class GameSettings
    {
        public static GameSettings Instance => instance ??= new GameSettings();
        private static GameSettings instance;

        public GameSettings()
        {
            Load();
        }

        public float masterVolume = 1;
        public float musicVolume = 1;
        public float sfxVolume = 1;
        public float sensitivity = 1;

        public bool useVoiceDialogue = true;
        public bool useSubtitles = true;

        public void Save()
        {
            string savedData = SnowSerializer.Serialize(this).Result;

            PlayerPrefs.SetString("GameSettings", savedData);
        }

        public void Load()
        {
            string savedData = PlayerPrefs.GetString("GameSettings");

            if (string.IsNullOrEmpty(savedData))
            {
                Debug.Log("No settings saved beforehand");
                return;
            }

            GameSettings settings = SnowSerializer.Deserialize<GameSettings>(savedData).Result;

            Instance.masterVolume = settings.masterVolume;
            Instance.musicVolume = settings.musicVolume;
            Instance.sfxVolume = settings.sfxVolume;
            Instance.sensitivity = settings.sensitivity;
            Instance.useVoiceDialogue = settings.useVoiceDialogue;
            Instance.useSubtitles = settings.useSubtitles;
        }
    }
}