// Created by Niels
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    [RequireComponent(typeof(BoxCollider))]
    public class TriggerNextVoiceLine : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                VoiceTutorialManager.Instance.PlayNextVoiceLine();
                enabled = false;
            }
        }
    }
}