using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public enum MenuState
    {
        Main,
        Settings,
        Credits
    }

    public MenuState state;
    private MenuState lastState;
    public float transitionSpeed = 4;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsMenu;

    [SerializeField] Vector2 normalPosition;
    [SerializeField] Vector2 hiddenPosition;

    public MainMenuController()
    {
        hiddenPosition = new Vector3(605, Screen.height * 2);
        normalPosition = new Vector2((Screen.width / 2) - (Screen.width / 4), Screen.height / 2);
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

        state = MenuState.Main;
    }

    // Update is called once per frame
    void Update()
    {
        normalPosition = new Vector2((Screen.width / 2) - (Screen.width / 6), (Screen.height / 2) + (Screen.width / 8));
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
        }
    }
}