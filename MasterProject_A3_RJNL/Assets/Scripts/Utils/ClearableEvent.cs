// Creator: Job
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using WinterRose;

namespace ShadowUprising.UnityUtils
{
    /// <summary>
    /// An event that returns the return value of all subscribers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClearableEvent<T>
    {
        List<Action<T>> events = new();

        /// <summary>
        /// Subscribes to the event.
        /// </summary>
        /// <param name="action"></param>
        public void Subscribe(Action<T> action)
        {
            events.Add(action);
        }

        /// <summary>
        /// Clears all subscribers from the event.
        /// </summary>
        public void Clear()
        {
            events.Clear();
        }

        /// <summary>
        /// INvokes the event
        /// </summary>
        /// <returns>All the return values from the subscribers in order of when they subscribed</returns>
        public void Invoke(T arg) => events.Foreach(e => e.Invoke(arg));

        public static ClearableEvent<T> operator +(ClearableEvent<T> evnt, Action<T> func)
        {
            evnt.Subscribe(func);
            return evnt;
        }
    }
}