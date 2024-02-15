// Creator: job

using UnityEngine;

namespace ShadowUprising.UnityUtils
{
    /// <summary>
    /// A singleton that is attached to a <see cref="GameObject"/> in Unity.
    /// </summary> 
    /// <typeparam name="TSelf">The type of <see cref="MonoBehaviour"/> the singleton is </typeparam>
    public class Singleton<TSelf> : MonoBehaviour where TSelf : MonoBehaviour
    {
        /// <summary>
        /// The instance of the singleton.
        /// </summary>
        public static TSelf Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (TSelf)(object)this;

                if (GetType().IsDefined(typeof(DontDestroyOnLoadAttribute), false))
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}