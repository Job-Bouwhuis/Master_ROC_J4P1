using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Items
{
    /// <summary>
    /// Defines the function of an item
    /// </summary>
    public interface IItemFunction
    {
        /// <summary>
        /// When derived, this function will execute the items use function
        /// </summary>
        void UseItem();
    }
}