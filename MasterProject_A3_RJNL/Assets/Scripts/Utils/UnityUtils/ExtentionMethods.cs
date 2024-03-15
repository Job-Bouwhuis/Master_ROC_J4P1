// Creator: Job

using System.Collections.Generic;
using UnityEngine;
using WinterRose;

#nullable enable

namespace ShadowUprising.UnityUtils
{
    public static class ExtentionMethods
    {
        /// <summary>
        /// Searches for all occurances of a component in the <paramref name="object"/> and its children recursively
        /// </summary>
        /// <typeparam name="T">The type of component to search for</typeparam>
        /// <param name="object">The object to search on</param>
        /// <returns>A collection of components that were found, collection is empty if no components of type <typeparamref name="T"/> were found</returns>
        public static IEnumerable<T> FindAllComponents<T>(this GameObject @object)
        {
            List<T> results = new();

            T? component = @object.GetComponent<T>();
            if (component != null)
                results.Add(component);

            foreach (Transform child in @object.transform)
            {
                IEnumerable<T> childResults = child.gameObject.FindAllComponents<T>();
                childResults.Foreach(x =>
                {
                    if(x != null)
                        results.Add(x);
                });
            }

            return results;
        }

        public static bool IsDestroyed(this GameObject obj) => obj.IsDead();
    }
}

namespace ShadowUprising
{
    using Unity.VisualScripting;

    internal static class ExtentionMethodsUsingVisualScriptingNamespace
    {
        public static bool IsDead(this GameObject obj) => obj.IsDestroyed();
    }
}