// Creator: job
using UnityEngine;
using WinterRose.Serialization;

namespace ShadowUprising.Settings
{
    public class GameSettings
    {
        /// <summary>
        /// The master volume of the game. 1 is full volume, 0 is no volume.
        /// </summary>
        public float masterVolume = 1;
        /// <summary>
        /// The volume of the music in the game. 1 is full volume, 0 is no volume.
        /// </summary>
        public float musicVolume = 1;
        /// <summary>
        /// The volume of the sound effects in the game. 1 is full volume, 0 is no volume.
        /// </summary>
        public float sfxVolume = 1;
        /// <summary>
        /// The sensitivity of the mouse in the game. 2 is double sensitivity, 1 is normal sensitivity, 0 is no sensitivity.
        /// </summary>
        public float sensitivity = 170;
        /// <summary>
        /// Whether or not to use voice dialogue in the game.
        /// </summary>
        public bool useVoiceDialogue = true;
        /// <summary>
        /// Whether or not to use subtitles in the game. (inside the tutorial subtitles are always on)
        /// </summary>
        public bool useSubtitles = true;

        /// <summary>
        /// The global instance of the game settings.
        /// </summary>
        public static GameSettings Instance => instance ??= new GameSettings();
        private static GameSettings instance;

        /// <summary>
        /// Makes a new instance of the game settings, and loads the settings into it.
        /// </summary>
        public GameSettings()
        {
            Load();
        }

        /// <summary>
        /// Sabes the current settings.
        /// </summary>
        public void Save()
        {
            PlayerPrefs.SetFloat("masterVolume", masterVolume);
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
            PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
            PlayerPrefs.SetFloat("sensitivity", sensitivity);

            PlayerPrefs.SetInt("useVoiceDialogue", useVoiceDialogue ? 1 : 0);
            PlayerPrefs.SetInt("useSubtitles", useSubtitles ? 1 : 0);
        }
        /// <summary>
        /// Loads the settings into this instance.
        /// </summary>
        public void Load()
        {
            if(PlayerPrefs.HasKey("masterVolume"))
                masterVolume = PlayerPrefs.GetFloat("masterVolume");
            else
                masterVolume = 1;

            if(PlayerPrefs.HasKey("musicVolume"))
                musicVolume = PlayerPrefs.GetFloat("musicVolume");
            else
                musicVolume = 1;

            if(PlayerPrefs.HasKey("sfxVolume"))
                sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            else
                sfxVolume = 1;

            if(PlayerPrefs.HasKey("sensitivity"))
                sensitivity = PlayerPrefs.GetFloat("sensitivity");
            else
                sensitivity = 1;

            if(PlayerPrefs.HasKey("useVoiceDialogue"))
                useVoiceDialogue = PlayerPrefs.GetInt("useVoiceDialogue") == 1;
            else
                useVoiceDialogue = true;

            if(PlayerPrefs.HasKey("useSubtitles"))
                useSubtitles = PlayerPrefs.GetInt("useSubtitles") == 1;
            else
                useSubtitles = true;
        }
    }
}