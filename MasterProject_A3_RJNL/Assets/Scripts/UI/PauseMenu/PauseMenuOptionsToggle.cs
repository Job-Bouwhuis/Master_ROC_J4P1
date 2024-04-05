// Creator: Job

namespace ShadowUprising.UI.PauseMenu
{
    /// <summary>
    /// Button function responsible for toggling the options screen on the pause menu
    /// </summary>
    public class PauseMenuOptionsToggle : ButtonFunction
    {
        public override void InvokeRelease(TextButton button)
        {
            if (PauseMenuManager.Instance.OptionsOpen)
                PauseMenuManager.Instance.CloseOptions();
            else
                PauseMenuManager.Instance.OpenOptions();
        }
    }
}