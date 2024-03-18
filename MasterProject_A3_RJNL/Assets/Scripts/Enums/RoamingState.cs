//Creator: Luke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI
{
    public enum RoamingState
    {
        /// <summary>
        /// makes the ai roam to random positions
        /// </summary>
        Random,
        /// <summary>
        /// makes the ai go from 0 to max postitions back from max to 0
        /// </summary>
        Sequential,
        /// <summary>
        /// makes the ai go from to 0 to max and instantly back to 0 and then up to max
        /// </summary>
        Loop
    }
}
