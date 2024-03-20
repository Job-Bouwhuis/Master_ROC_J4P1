using ShadowUprising.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI.PauseMenu
{
    /// <summary>
    /// Button function responsible for toggling the options screen on the pause menu
    /// </summary>
    public class PauseMenuOptionsToggle : ButtonFunction
    {
        public override void Invoke(TextButton button)
        {
            if (PauseMenuManager.Instance.OptionsOpen)
                PauseMenuManager.Instance.CloseOptions();
            else
                PauseMenuManager.Instance.OpenOptions();
        }
    }
}