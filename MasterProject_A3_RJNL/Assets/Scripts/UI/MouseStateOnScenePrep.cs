// Creator: Job
using ShadowUprising;
using ShadowUprising.UI.Loading;
using UnityEngine;

namespace ShadowUprising.ScenePreps
{
    /// <summary>
    /// Scene prep that sets the mouse state on the scene to the specified state.
    /// </summary>
    public class MouseStateOnScenePrep : MonoBehaviour, IScenePrepOperation
    {
        /// <summary>
        /// The mode the mouse should be in
        /// </summary>
        public CursorLockMode mode = CursorLockMode.None;
        /// <summary>
        /// The visibility of the cursor
        /// </summary>
        public bool showCursor = false;

        public bool IsComplete { get; set; }

        public void FinishPrep() { }

        public void StartPrep()
        {
            Cursor.lockState = mode;
            Cursor.visible = showCursor;

            Log.Push("Mouse state switched to: " + mode.ToString());
        }

        public YieldInstruction Update()
        {
            return new Completed();
        }
    }
}