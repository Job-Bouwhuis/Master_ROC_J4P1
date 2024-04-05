// Creator: Job
using UnityEngine;
using WinterRose;

namespace ShadowUprising.UI.ButtonFunctions
{
    /// <summary>
    /// Button function to close the game.
    /// </summary>
    public class CloseGame : ButtonFunction
    {
        /// <summary>
        /// On invoke, closes the game.
        /// <br></br> In the editor, it will give a message box dialog that the game would be closed in the build.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="isToggle"></param>
        public override void InvokeRelease(TextButton button)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
            return;
#endif

            // This code is only executed in the build. In the editor,
            // it will give a warning that the code is unreachable. this can be ignored.
            Application.Quit();
        }
    }
}