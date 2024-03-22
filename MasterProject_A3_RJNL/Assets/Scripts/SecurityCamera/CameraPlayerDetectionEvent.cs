//Creator: Ruben
using WinterRose;
using ShadowUprising.UI.SpottingIndicator;
using ShadowUprising.GameOver;
using UnityEngine;
using ShadowUprising.Detection;

namespace ShadowUprising.SecurityCamera
{
    /// <summary>
    /// The component managing the detection the functionality called when the camera detects a player or body
    /// </summary>
    public class CameraPlayerDetectionEvent : MonoBehaviour
    {
        float timer;
        bool detecting;
        [Tooltip("The speed at which the detection process occurs in seconds")]
        [SerializeField] float detectionSpeed;
        [Tooltip("The amount the timer goes down per second")]
        [SerializeField] float timerDecreaseSpeed;

        void Start()
        {
            Asign();
        }

        void Asign()
        {
            AI.AIPlayerConeDetector playerConeDetector = GetComponentInChildren<AI.AIPlayerConeDetector>(); 
            playerConeDetector.onObjectDetected += OnObjectDetected;
            playerConeDetector.onNothingDetected += OnNothingDetected;
            if (DetectionManager.Instance == null)
            {
                Windows.MessageBox("Please add DetectionManager to the scene. This is required for the security camera to function", "Caution", Windows.MessageBoxButtons.OK, Windows.MessageBoxIcon.Exclamation);
            }
            else
                detectionSpeed = DetectionManager.Instance.detectionSpeed;
        }

        void OnObjectDetected(GameObject gameObject)
        {
            if (detecting == false)
                if (DetectionManager.Instance != null)
                    DetectionManager.Instance.AddDetecting(this.gameObject);
            detecting = true;
            timer += Time.deltaTime;

            if (timer >= detectionSpeed && GameOverManager.Instance != null)
                GameOverManager.Instance.GameOver();
        }

        void OnNothingDetected()
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