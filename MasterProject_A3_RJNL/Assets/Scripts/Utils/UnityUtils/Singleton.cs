// Creator: job

using UnityEngine;

namespace ShadowUprising.UnityUtils
{
    /// <summary>
    /// A singleton that is attached to a <see cref="GameObject"/> in Unity.
    /// </summary> 
    /// <typeparam name="TSelf">The type of <see cref="MonoBehaviour"/> the singleton is </typeparam>
    public abstract class Singleton<TSelf> : MonoBehaviour where TSelf : MonoBehaviour
    {
        /// <summary>
        /// The instance of the singleton.
        /// </summary>
        public static TSelf Instance { get; protected set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as TSelf;

                if (GetType().IsDefined(typeof(DontDestroyOnLoadAttribute), false))
                    DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}