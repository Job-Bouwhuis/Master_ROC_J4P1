using UnityEngine;
using ShadowUprising.UI;
using ShadowUprising.UI.PauseMenu;

namespace ShadowUprising.UI.ButtonFunctions
{
    public class ClosePauseMenu : ButtonFunction
    {
        public override void Invoke(TextButton button)
        {
            PauseMenuManager.Instance.Unpause();
        }
    }
}