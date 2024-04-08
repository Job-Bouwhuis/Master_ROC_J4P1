using ShadowUprising.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.DEBUG
{
    /// <summary>
    /// Used to test the DeadGuardDetectedIndicator component
    /// </summary>
    public class DEBUGShowGuardDetectedAlert : MonoBehaviour
    {
        [SerializeField, Tooltip("Activates the UI by pressing G")] DeadGuardDetectedIndicator indicator;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
                indicator.TriggerAlert();
        }
    }
}