// Creator: Job
using UnityEngine;
using UnityEngine.UI;
using ShadowUprising.Player;

namespace ShadowUprising.UI.InGame
{
    /// <summary>
    /// The class responsible for displaying the health and stamina of the player on the UI when required.
    /// </summary>
    public class HealthAndStaminaDisplayer : MonoBehaviour
    {
        /// <summary>
        /// To be changed to the actual player once this is implemented by Ruben.
        /// </summary>
        [Header("Player")]
        [Tooltip("The player to display the health and stamina of. (To be changed to the actual player script.)")]
        public PlayerStats player;

        [Header("UI Elements")]
        [SerializeField] private ElementAnimator healthAndStaminaElement;
        [SerializeField] private Image healthBar;
        [SerializeField] private Image staminaBar;

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
                healthBar.rectTransform.sizeDelta = new Vector2(healthBar.rectTransform.sizeDelta.x, healthHeightMax * ((float)player.health / player.maxHealth));
                lastHealth = player.health;

                healthAndStaminaElement.Show();
            }

            if (lastStamina != player.stamina)
            {
                staminaBar.rectTransform.sizeDelta = new Vector2(staminaBar.rectTransform.sizeDelta.x, staminaHeightMax * (player.stamina / player.maxStamina));
                lastStamina = player.stamina;

                healthAndStaminaElement.Show();
            }
        }
    }
}