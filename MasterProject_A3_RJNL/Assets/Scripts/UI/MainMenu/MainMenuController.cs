// Creator: Job
using System.Collections;
using UnityEngine;

namespace ShadowUprising.UI.MainMenu
{
    /// <summary>
    /// The main menu controller, handles the state of the main menu and transitions between the main menu, settings, and credits.
    /// </summary>
    public class MainMenuController : MonoBehaviour
    {
        /// <summary>
        /// The state of the main menu, used to determine which menu is currently active.
        /// </summary>
        public enum MenuState
        {
            /// <summary>
            /// No menu is currently active, used for hiding all menus.
            /// </summary>
            None,
            /// <summary>
            /// The main menu, where Play, Settings, and Credits are displayed.
            /// </summary>
            Main,
            /// <summary>
            /// The settings, where things like volume and sensitivity can be adjusted.
            /// </summary>
            Settings,
            /// <summary>
            /// The credits, where the creators of the game are displayed.
            /// </summary>
            Credits
        }

        [Tooltip("The current state of the main menu")] public MenuState state;
        [Tooltip("The time it takes to swtich from one menu to another")] public float transitionSpeed = 4;

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject settingsMenu;
        [SerializeField] private GameObject creditsMenu;

        Vector2 normalPosition;
        Vector2 hiddenPosition;

        /// <summary>
        /// The constructor for the main menu controller, sets the hidden and normal positions for the menus.
        /// </summary>
        public MainMenuController()
        {
            hiddenPosition = new Vector3(605, Screen.height * 2);
            normalPosition = new Vector2(Screen.width / 2 - Screen.width / 4, Screen.height / 2);
        }

        // Start is called before the first frame update
        void Start()
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(true);
            creditsMenu.SetActive(true);

            mainMenu.transform.position = normalPosition;
            settingsMenu.transform.position = hiddenPosition;
            creditsMenu.transform.position = hiddenPosition;

            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
            creditsMenu.SetActive(false);

            state = MenuState.Main;
            StartCoroutine(SetMenusActiveAgain());
        }

        // Update is called once per frame
        void Update()
        {
            UpdateMenus();
        }

        private IEnumerator SetMenusActiveAgain()
        {
            yield return new WaitForSecondsRealtime(1);
            settingsMenu.SetActive(true);
            creditsMenu.SetActive(true);
        }

        private void UpdateMenus()
        {
            normalPosition = new Vector2(Screen.width / 2 - Screen.width / 6, Screen.height / 2 + Screen.width / 8);
            hiddenPosition = new Vector3(605, Screen.height * 2);

            // lerp the current menu to the normal position, and the last menu to the hidden position
            switch (state)
            {
                case MenuState.Main:
                    mainMenu.transform.position = Vector2.Lerp(mainMenu.transform.position, normalPosition, transitionSpeed * Time.deltaTime);
                    settingsMenu.transform.position = Vector2.Lerp(settingsMenu.transform.position, hiddenPosition, transitionSpeed * Time.deltaTime);
                    creditsMenu.transform.position = Vector2.Lerp(creditsMenu.transform.position, hiddenPosition, transitionSpeed * Time.deltaTime);
                    break;
                case MenuState.Settings:
                    mainMenu.transform.position = Vector2.Lerp(mainMenu.transform.position, hiddenPosition, transitionSpeed * Time.deltaTime);
                    settingsMenu.transform.position = Vector2.Lerp(settingsMenu.transform.position, normalPosition, transitionSpeed * Time.deltaTime);
                    creditsMenu.transform.position = Vector2.Lerp(creditsMenu.transform.position, hiddenPosition, transitionSpeed * Time.deltaTime);
                    break;
                case MenuState.Credits:
                    mainMenu.transform.position = Vector2.Lerp(mainMenu.transform.position, hiddenPosition, transitionSpeed * Time.deltaTime);
                    settingsMenu.transform.position = Vector2.Lerp(settingsMenu.transform.position, hiddenPosition, transitionSpeed * Time.deltaTime);
                    creditsMenu.transform.position = Vector2.Lerp(creditsMenu.transform.position, normalPosition, transitionSpeed * Time.deltaTime);
                    break;
                case MenuState.None:
                    mainMenu.transform.position = Vector2.Lerp(mainMenu.transform.position, hiddenPosition, transitionSpeed * Time.deltaTime);
                    settingsMenu.transform.position = Vector2.Lerp(settingsMenu.transform.position, hiddenPosition, transitionSpeed * Time.deltaTime);
                    creditsMenu.transform.position = Vector2.Lerp(creditsMenu.transform.position, hiddenPosition, transitionSpeed * Time.deltaTime);
                    break;
            }
        }
    }
}