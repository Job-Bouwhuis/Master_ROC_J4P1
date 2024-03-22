using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI.Loading
{
    /// <summary>
    /// An interface for a scene preperation operation. used to prepare a scene after it has been loaded.<br></br>
    /// eg: randomize loot positions, reset player position, reset time scale, lock mouse, etc.
    /// </summary>
    public interface IScenePrepOperation
    {
        /// <summary>
        /// True when the operation is complete
        /// </summary>
        bool IsComplete { get; set; }

        /// <summary>
        /// Initialize the operation. called once when the preperation starts, and before any preperation update is called.
        /// </summary>
        void StartPrep();

        /// <summary>
        /// Update the operation. called every frame until the operation is complete.
        /// </summary>
        /// <returns>A <see cref="YieldInstruction"/> used to be able to wait like it is a coroutine.<br></br>
        /// Return <see cref="Completed"/> to have the system set <see cref="IsComplete"/> to true. 
        /// setting <see cref="IsComplete"/> manually would work the same</returns>
        YieldInstruction PrepUpdate();

        /// <summary>
        /// Finalize the operation. called once when the preperation is complete, and after the last update is called.
        /// </summary>
        void FinishPrep();
    }
}