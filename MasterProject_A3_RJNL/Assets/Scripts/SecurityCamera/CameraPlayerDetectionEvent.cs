//Creator: Ruben
using WinterRose;
using UnityEngine;
using ShadowUprising.Detection;

namespace ShadowUprising.SecurityCamera
{
    public class CameraPlayerDetectionEvent : MonoBehaviour
    {
        float timer;
        bool detecting;
        [SerializeField] float detectionSpeed;
        [SerializeField] float timerDecreaseSpeed;
        [SerializeField] UI.SpottingIndicator.DetectionIndicator detectionIndicator;


        void Start()
        {
            if (DetectionManager.Instance == null)
            {
                Windows.MessageBox("Please add DetectionManager to the scene. This is required for the security camera to function", "Caution", Windows.MessageBoxButtons.OK, Windows.MessageBoxIcon.Exclamation);
            }
            Asign();
        }

        void Asign()
        {
            AI.AIPlayerConeDetector playerConeDetector = GetComponentInChildren<AI.AIPlayerConeDetector>(); 
            playerConeDetector.onPlayerDetected += OnPlayerDetected;
            playerConeDetector.onPlayerNotDetected += OnPlayerNotDetected;
            if (detectionIndicator != null)
                detectionSpeed = detectionIndicator.detectionSpeed;
        }

        void OnPlayerDetected(Vector3 playerPos)
        {
            timer += Time.deltaTime;
            if (detecting == false)
                if (DetectionManager.Instance != null)
                    DetectionManager.Instance.AddDetecting(this.gameObject);
            detecting = true;
        }

        void OnPlayerNotDetected()
        {
            if (timer > 0)
                timer -= timerDecreaseSpeed * Time.deltaTime;
            if (detecting)
            {
                detecting = false;
                if (DetectionManager.Instance != null)
                    DetectionManager.Instance.RemoveDetecting(this.gameObject);
            }
        }
    }
}