// Created by Niels
using ShadowUprising.Inventory;
using ShadowUprising.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    public class TutorialAndLoreVoiceLinesAtStart : MonoBehaviour
    {
        [SerializeField] Item gun;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(PlayTutorialAndLoreVoiceLines());
        }

        IEnumerator PlayTutorialAndLoreVoiceLines()
        {
            float duration = VoiceTutorialManager.Instance.PlayNextVoiceLine(); // 1
            yield return new WaitForSecondsRealtime(duration + 0.4f);

            duration = VoiceTutorialManager.Instance.PlayNextVoiceLine(); // 2
            yield return new WaitForSecondsRealtime(duration);
            InventoryManager.Instance.AddItem(gun);
            yield return new WaitForSecondsRealtime(0.8f);

            duration = VoiceTutorialManager.Instance.PlayNextVoiceLine(); // 3
            yield return new WaitForSecondsRealtime(duration + 0.2f);

            duration = VoiceTutorialManager.Instance.PlayNextVoiceLine(); // 4
            yield return new WaitForSecondsRealtime(duration + 0.4f);

            duration = VoiceTutorialManager.Instance.PlayNextVoiceLine(); // 5
            yield return new WaitForSecondsRealtime(duration + 0.1f);

            duration = VoiceTutorialManager.Instance.PlayNextVoiceLine(); // 6
            yield return new WaitForSecondsRealtime(duration + 0.3f);

            duration = VoiceTutorialManager.Instance.PlayNextVoiceLine(); // 7
            yield return new WaitForSecondsRealtime(duration + 0.2f);

            duration = VoiceTutorialManager.Instance.PlayNextVoiceLine(); // 8
        }
    }
}