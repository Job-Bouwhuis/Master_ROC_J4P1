// Creator: Job
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI
{
    /// <summary>
    /// A base class for button functions
    /// </summary>
    public abstract class ButtonFunction : MonoBehaviour
    {
        /// <summary>
        /// When overriden in a derived class, this method will execute a function when a button is clicked
        /// </summary>
        /// <param name="button">The calling button</param>
        public abstract void Invoke(TextButton button);
    }
}