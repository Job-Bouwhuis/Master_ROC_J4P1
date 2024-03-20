using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinterRose;

namespace ShadowUprising.Utils
{
    public class ClearableEvent<T>
    {
        List<Action<T>> subscribers = new();

        public void Subscribe(Action<T> action) => subscribers.Add(action);
        public void Unsubscribe(Action<T> action) => subscribers.Remove(action);
        public void Invoke(T arg) => subscribers.Foreach(subscriber => subscriber.Invoke(arg));
        public void InvokeParallel(T arg) => Parallel.ForEach(subscribers, subscriber => subscriber.Invoke(arg));
        public void Clear() => subscribers.Clear();

        public static ClearableEvent<T> operator +(ClearableEvent<T> clearableEvent, Action<T> action)
        {
            clearableEvent.Subscribe(action);
            return clearableEvent;
        }
    }
}