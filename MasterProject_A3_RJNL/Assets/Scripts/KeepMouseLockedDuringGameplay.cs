using ShadowUprising.GameOver;
using ShadowUprising.UI.PauseMenu;
using TMPro;
using UnityEngine;

public class KeepMouseLockedDuringGameplay : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(PauseMenuManager.Instance != null && PauseMenuManager.Instance.IsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if(GameOverManager.Instance != null && GameOverManager.Instance.IsGameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            enabled = false;
        }
    }
} 
