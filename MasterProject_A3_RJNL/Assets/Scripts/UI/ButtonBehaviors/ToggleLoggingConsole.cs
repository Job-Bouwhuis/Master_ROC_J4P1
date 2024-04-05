// Creator: job
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    /// <summary>
    /// Button function to toggle the log console on and off. Only works in the build
    /// </summary>
    public class ToggleLoggingConsole : ButtonFunction
    {
        private void Awake()
        {
            GetComponent<TextButton>().toggleState = false;
        }

        public override void InvokeRelease(TextButton button)
        {
            Log.ConsoleEnabled = button.toggleState;

#if UNITY_EDITOR
            button.toggleState = false;
#endif
        }
    }
}