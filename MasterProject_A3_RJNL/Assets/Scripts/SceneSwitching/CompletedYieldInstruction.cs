// Creator: Job
using UnityEngine;

namespace ShadowUprising.UI.Loading
{
    /// <summary>
    /// A yield instruction that instructs the loading operation that this preperation step is completed.
    /// <br></br> used in <see cref="IScenePrepOperation"/> to signal that the operation is completed.
    /// </summary>
    public class Completed : YieldInstruction
    {
        /// <summary>
        /// A yield instruction that instructs the loading operation that this preperation step is completed.
        /// <br></br> used in <see cref="IScenePrepOperation"/> to signal that the operation is completed.
        /// </summary>
        public Completed() { }
    }
}