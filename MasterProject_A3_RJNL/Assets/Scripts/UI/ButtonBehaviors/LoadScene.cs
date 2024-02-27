// Creator: Job
using ShadowUprising.UI.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI.ButtonFunctions
{
    public class LoadScene : ButtonFunction
    {
        public string sceneName;

        public override void Invoke(TextButton button)
        {
            LoadingScreen.Instance.ShowAndLoad(sceneName);
        }
    }
}