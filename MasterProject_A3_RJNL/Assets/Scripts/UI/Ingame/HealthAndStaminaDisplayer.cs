using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShadowUprising.UI.InGame
{
    /// <summary>
    /// The class responsible for displaying the health and stamina of the player on the UI when required.
    /// </summary>
    public class HealthAndStaminaDisplayer : MonoBehaviour
    {
        [SerializeField] private ElementAnimator healthAndStaminaElement;

        [SerializeField] private Image healthBar;
        [SerializeField] private Image staminaBar;

        /// <summary>
        /// To be changed to the actual player once this is implemented by Ruben.
        /// </summary>
        public DebugPlayerHealth player;

        private float lastHealth;
        private float lastStamina;

        private float healthHeightMax;
        private float staminaHeightMax;

        private void Start()
        {
            lastHealth = player.health;
            lastStamina = player.stamina;

            healthHeightMax = healthBar.rectTransform.sizeDelta.y;
            staminaHeightMax = staminaBar.rectTransform.sizeDelta.y;
        }

        private void Update()
        {
            if (lastHealth != player.health)
            {
                healthBar.rectTransform.sizeDelta = new Vector2(healthBar.rectTransform.sizeDelta.x, healthHeightMax * (player.health / 500f));
                lastHealth = player.health;

                healthAndStaminaElement.Show();
            }

            if (lastStamina != player.stamina)
            {
                staminaBar.rectTransform.sizeDelta = new Vector2(staminaBar.rectTransform.sizeDelta.x, staminaHeightMax * (player.stamina / 500f));
                lastStamina = player.stamina;

                healthAndStaminaElement.Show();
            }
        }
    }
}