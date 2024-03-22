// Creator: Job
using ShadowUprising;
using ShadowUprising.UI.Loading;
using UnityEngine;

namespace ShadowUprising.ScenePreps
{
    public class MouseStateOnScenePrep : MonoBehaviour, IScenePrepOperation
    {
        public CursorLockMode lockMode = CursorLockMode.None;
        public bool showCursor = false;

        public bool IsComplete { get; set; }

        public void FinishPrep() { }

        public void StartPrep()
        {
            Cursor.lockState = lockMode;
            Cursor.visible = showCursor;

            Log.Push("Mouse state switched to: " + lockMode.ToString());
        }

        public YieldInstruction Update()
        {
            return new Completed();
        }
    }
}