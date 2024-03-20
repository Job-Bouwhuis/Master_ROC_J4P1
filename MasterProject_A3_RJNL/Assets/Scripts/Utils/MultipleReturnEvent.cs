// Creator: Job
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace ShadowUprising.UnityUtils
{
    /// <summary>
    /// An event that returns the return value of all subscribers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MultipleReturnEvent<T>
    {
        List<Func<T>> events = new();

        /// <summary>
        /// Subscribes to the event.
        /// </summary>
        /// <param name="action"></param>
        public void Subscribe(Func<T> action)
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
        public List<T> Invoke()
        {
            List<T> result = new();

            foreach (var e in events)
            {
                result.Add(e());
            }

            return result;
        }

        /// <summary>
        /// Subscribes to the event.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static MultipleReturnEvent<T> operator +(MultipleReturnEvent<T> evnt, Func<T> func)
        {
            evnt.Subscribe(func);
            return evnt;
        }
    }
}