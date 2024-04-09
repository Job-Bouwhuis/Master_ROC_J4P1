// Created by Niels
using ShadowUprising.UnityUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class VoiceTutorialManager : Singleton<VoiceTutorialManager>
    {
        [SerializeField] List<AudioClip> voiceLines = new List<AudioClip>();
        private AudioSource audioSource;
        int currentIndex = -1;

        protected override void Awake()
        {
            base.Awake();   
            audioSource = GetComponent<AudioSource>();
        }

        public float PlayNextVoiceLine()
        {
            if (currentIndex < voiceLines.Count - 1)
            {
                currentIndex++;
                audioSource.clip = voiceLines[currentIndex];
                audioSource.Play();
                float duration = audioSource.clip.length;
                StartCoroutine(PauseGame(duration));
                return duration;
            }
            return 0;
        }

        private IEnumerator PauseGame(float duration)
        {
              Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1;
        }
    }
}