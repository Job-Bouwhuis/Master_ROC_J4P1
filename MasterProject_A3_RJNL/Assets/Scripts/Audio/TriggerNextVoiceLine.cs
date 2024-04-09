// Created by Niels
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    [RequireComponent(typeof(BoxCollider))]
    public class TriggerNextVoiceLine : MonoBehaviour
    {
        bool triggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (triggered) return;

            if (other.CompareTag("Player"))
            {
                triggered = true;
                VoiceTutorialManager.Instance.PlayNextVoiceLine();
                enabled = false;
            }
        }
    }
}