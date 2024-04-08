// Creator: Job

using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    /// <summary>
    /// A button function that opens a link in the default browser.
    /// </summary>
    public class OpenLink : ButtonFunction
    {
        /// <summary>
        /// The link to open.
        /// </summary>
        [Tooltip("The link that will be opened on invoking this function")]
        public string link;

        public override void InvokeRelease(TextButton button) => Application.OpenURL(link);
    }
}