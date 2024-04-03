using ShadowUprising;
using ShadowUprising.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.DEBUG
{
    /// <summary>
    /// Logs a message when the item is interacted with. This is a <b>DEBUG</b> class and should not be used in the final game.
    /// </summary>
    public class DEBUGItemInteraction : MonoBehaviour, IItemFunction
    {
        public void UseItem()
        {
            Log.Push("Item was interacted with");
        }
    }
}