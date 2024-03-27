// Creator: Job
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinterRose;
namespace ShadowUprising.Utils
{
    /// <summary>
    /// An event that can be cleared of all its subscribers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClearableEvent<T>
    {
        List<Action<T>> subscribers = new();

        /// <summary>
        /// Adds the passed action as a subscriber to this event
        /// </summary>
        /// <param name="action"></param>
        public void Subscribe(Action<T> action) => subscribers.Add(action);
        /// <summary>
        /// Removes the passed action from the subscribers of this event
        /// </summary>
        /// <param name="action"></param>
        public void Unsubscribe(Action<T> action) => subscribers.Remove(action);
        /// <summary>
        /// Invokes all the subscribers of this event
        /// </summary>
        /// <param name="arg"></param>
        public void Invoke(T arg) => subscribers.Foreach(subscriber =>
        {
            try
            {
                subscriber.Invoke(arg);
            }
            catch (Exception e)
            {
                Log.PushError($"An error occurred while invoking a ClearableEvent: {e.GetType().FullName}");
            }
        });
        /// <summary>
        /// Invokes all the subscribers of this event in parallel
        /// </summary>
        /// <param name="arg"></param>
        public void InvokeParallel(T arg) => Parallel.ForEach(subscribers, subscriber => subscriber.Invoke(arg));
        /// <summary>
        /// Clears all the subscribers of this event
        /// </summary>
        public void Clear() => subscribers.Clear();

        /// <summary>
        /// Adds the passed action as a subscriber to this event
        /// </summary>
        /// <param name="clearableEvent"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ClearableEvent<T> operator +(ClearableEvent<T> clearableEvent, Action<T> action)
        {
            clearableEvent.Subscribe(action);
            return clearableEvent;
        }
    }
}