// Creator: Job
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UI.Loading
{
    /// <summary>
    /// Interface for scene prep operations. These are used to prepare a scene during the loading process.
    /// </summary>
    public interface IScenePrepOperation
    {
        /// <summary>
        /// Whether the operation is complete.
        /// </summary>
        bool IsComplete { get; set; }

        /// <summary>
        /// Called to start the operation.
        /// </summary>
        void StartPrep();

        /// <summary>
        /// Called each frame until the operation is complete.
        /// </summary>
        /// <returns>Anything you can yield return in a standard unity <see cref="Coroutine"/>.<br></br>
        /// In addition you can return <see cref="Completed"/> to have the preperation system set the <see cref="IsComplete"/> to true</returns>
        YieldInstruction Update();

        /// <summary>
        /// Called once when all prep operations are complete.
        /// </summary>
        void FinishPrep();
    }
}