using UnityEngine;
using ShadowUprising.UI;
using ShadowUprising.UI.PauseMenu;

namespace ShadowUprising.UI.ButtonFunctions
{
    /// <summary>
    /// Button function to close the pause menu.
    /// </summary>
    public class ClosePauseMenu : ButtonFunction
    {
        public override void Invoke(TextButton button)
        {
            PauseMenuManager.Instance.Unpause();
        }
    }
}