// Creator: Job
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI
{
    public abstract class ButtonFunction : MonoBehaviour
    {
        public abstract void Invoke(TextButton button, bool isToggle);
    }
}