//Created by: Niels
using ShadowUprising.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ShadowUprising.Audio
{
    public class ButtonAudio : ButtonFunction
    {
        [SerializeField] AudioClip click;
        [SerializeField] AudioClip hover;
        [SerializeField] AudioClip release;
        AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            TextButton textButton = GetComponent<TextButton>();
            textButton.OnHover += PlayHoverSound;
        }

        private void PlayHoverSound(bool obj)
        {
            if (obj)
            {
                audioSource.clip = hover;
                audioSource.Play();
            }

        }

        public override void InvokeRelease(TextButton button)
        {
            audioSource.clip = release;
            audioSource.Play();
            base.InvokeClick(button);
        }

        public override void InvokeClick(TextButton button)
        {
            audioSource.clip = click;
            audioSource.Play();
            base.InvokeClick(button);
        }
    }
}