// Creator: job
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    public class ToggleLoggingConsole : ButtonFunction
    {
        private void Awake()
        {
            GetComponent<TextButton>().toggleState = false;
        }

        public override void Invoke(TextButton button)
        {
            Log.ConsoleEnabled = button.toggleState;

#if UNITY_EDITOR
            button.toggleState = false;
#endif
        }
    }
}