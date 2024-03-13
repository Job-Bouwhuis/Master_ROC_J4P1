//Creator: Ruben
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.SecurityCamera
{
    public class CameraPlayerDetectionEvent : MonoBehaviour
    {
        public float timer;
        [SerializeField] float timerDecreaseSpeed;

        void Start()
        {
            Asign();
        }

        void Asign()
        {
            AI.AIPlayerConeDetector playerConeDetector = GetComponent<AI.AIPlayerConeDetector>();
            playerConeDetector.onPlayerDetected += OnPlayerDetected;
            playerConeDetector.onPlayerNotDetected += OnPlayerNotDetected;
        }

        void OnPlayerDetected(Vector3 playerPos)
        {
            timer += Time.deltaTime;
        }

        void OnPlayerNotDetected()
        {
            if (timer > 0)
            timer -= timerDecreaseSpeed * Time.deltaTime;
        }
    }
}