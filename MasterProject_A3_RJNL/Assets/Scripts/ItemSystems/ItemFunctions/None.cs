// Creator: Job
using UnityEngine;

namespace ShadowUprising.Items.ItemFunctions
{
    /// <summary>
    /// Represents a item function that does nothing,
    /// </summary>
    public class None : MonoBehaviour, IItemFunction
    {
        public void UseItem() { }
    }
}