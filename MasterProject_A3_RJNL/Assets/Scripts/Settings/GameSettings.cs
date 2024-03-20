// Creator: job
using ShadowUprising.Utils;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using WinterRose.Serialization;

namespace ShadowUprising.Settings
{
    public class GameSettings
    {
        /// <summary>
        /// Invoked when one of the settings is changed.<br></br>
        /// The passed <see cref="bool"/> is always true.
        /// </summary>
        public ClearableEvent<bool> OnSettingsChanged = new();

        /// <summary>
        /// The master volume of the game. 1 is full volume, 0 is no volume.
        /// </summary>
        public float MasterVolume
        {
            get => masterVolume;
            set
            {
                masterVolume = value;
                OnSettingsChanged?.Invoke(true);
            }
        }
        private float masterVolume = 1;
        /// <summary>
        /// The volume of the music in the game. 1 is full volume, 0 is no volume.
        /// </summary>
        public float MusicVolume
        {
            get => musicVolume;
            set
            {
                musicVolume = value;
                OnSettingsChanged?.Invoke(true);
            }
        }
        private float musicVolume = 1;
        /// <summary>
        /// The volume of the sound effects in the game. 1 is full volume, 0 is no volume.
        /// </summary>
        public float SfxVolume
        {
            get => sfxVolume;
            set
            {
                sfxVolume = value;
                OnSettingsChanged?.Invoke(true);
            }
        }
        private float sfxVolume = 1;
        /// <summary>
        /// The sensitivity of the mouse in the game. 2 is double sensitivity, 1 is normal sensitivity, 0 is no sensitivity.
        /// </summary>
        public float Sensitivity
        {
            get => sensitivity;
            set
            {
                sensitivity = value;
                OnSettingsChanged?.Invoke(true);
            }
        }
        private float sensitivity = 170;
        /// <summary>
        /// Whether or not to use voice dialogue in the game.
        /// </summary>
        public bool UseVoiceDialogue
        {
            get => useVoiceDialogue;
            set
            {
                useVoiceDialogue = value;
                OnSettingsChanged?.Invoke(true);
            }
        }
        private bool useVoiceDialogue = true;
        /// <summary>
        /// Whether or not to use subtitles in the game. (inside the tutorial subtitles are always on)
        /// </summary>
        public bool UseSubtitles
        {
            get => useSubtitles;
            set
            {
                useSubtitles = value;
                OnSettingsChanged?.Invoke(true);
            }
        }
        private bool useSubtitles = true;

        /// <summary>
        /// Updates the settings that are not null. if a setting is null, it will not be updated.<br></br>
        /// calls the <see cref="OnSettingsChanged"/> event only once. instead of after every setting change 
        /// like you would get by doing it manually.
        /// </summary>
        /// <param name="masterVolume"></param>
        /// <param name="musicVolume"></param>
        /// <param name="sfxVolume"></param>
        /// <param name="sensitivity"></param>
        /// <param name="useVoiceDialogue"></param>
        /// <param name="useSubtitles"></param>
        public void UpdateSettingsBulk(float? masterVolume = null,
                                       float? musicVolume = null,
                                       float? sfxVolume = null,
                                       float? sensitivity = null,
                                       bool? useVoiceDialogue = null,
                                       bool? useSubtitles = null)
        {
            if(masterVolume != null)
                this.masterVolume = masterVolume.Value;
            if(musicVolume != null)
                this.musicVolume = musicVolume.Value;
            if(sfxVolume != null)
                this.sfxVolume = sfxVolume.Value;
            if(sensitivity != null)
                this.sensitivity = sensitivity.Value;
            if(useVoiceDialogue != null)
                this.useVoiceDialogue = useVoiceDialogue.Value;
            if(useSubtitles != null)
                this.useSubtitles = useSubtitles.Value;

            OnSettingsChanged?.Invoke(true);
        }

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
            Save();
        }

        /// <summary>
        /// Saves the current settings.
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
                sensitivity = 170;

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