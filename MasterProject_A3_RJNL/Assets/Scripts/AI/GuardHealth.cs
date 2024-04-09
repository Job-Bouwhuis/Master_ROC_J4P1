// Creator: Ruben
using ShadowUprising.Audio;
using ShadowUprising.UI.InfoDialogs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    /// <summary>
    /// This components holds the current health of the guard and destroys it on death. replacing it with a dead guard
    /// </summary>
    public class GuardHealth : MonoBehaviour
    {
        [Tooltip("Current health of the guard. Set this value to change how many hits the guard can take")]
        public int health;
        [Tooltip("Reference to the prefab that should be instantiated when the guard dies")]
        public GameObject deadGuard;

        private void Update()
        {
            CheckDeath();
        }

        void CheckDeath()
        {
            if (health <= 0)
            {
                Instantiate(deadGuard, transform.position, transform.rotation);
                Destroy(this.gameObject);

                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TutorialLevelScene")
                {
                    InfoDialogManager.Instance.ShowInfoDialog("TUTORIAL", "Other guards will see dead bodies.\n" +
                        "You can pick them up by looking at them and pressing 'F'.\n" +
                        "Drop them again by pressing 'X'", 5);
                    InfoDialogManager.Instance.ShowInfoDialog("TUTORIAL", "To proceed, walk through the door the guard came from!", 5);
                    VoiceTutorialManager.Instance.PlayNextVoiceLine();
                }
            }
        }

        /// <summary>
        /// Add health the the guard
        /// </summary>
        /// <param name="addedHealth">Amount of health to be added to guard</param>
        public void AddHealth(int addedHealth)
        {
            health += addedHealth;
        }

        /// <summary>
        /// Remove health from guard
        /// </summary>
        /// <param name="removedHealth">Amount of health to be removed from guard</param>
        public void RemoveHealth(int removedHealth)
        {
            health -= removedHealth;
        }
    }
}