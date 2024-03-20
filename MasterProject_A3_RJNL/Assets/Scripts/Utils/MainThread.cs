using UnityEngine;
using System;
using System.Collections.Generic;
using ShadowUprising.UnityUtils;

namespace ShadowUprising.Utils
{
    /// <summary>
    /// Allows to execute methods on the main thread from any other thread.
    /// </summary>
    public class MainThread : Singleton<MainThread>
    {
        private static readonly Queue<Action> ActionQueue = new Queue<Action>();

        private static MainThread _instance;

        public static void Invoke(Action action)
        {
            lock (ActionQueue)
            {
                ActionQueue.Enqueue(action);
            }
        }

        private void Update()
        {
            lock (ActionQueue)
            {
                while (ActionQueue.Count > 0)
                {
                    ActionQueue.Dequeue()?.Invoke();
                }
            }
        }
    }
}