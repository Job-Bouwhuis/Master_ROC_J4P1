// Creator: Job
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowUprising.Items
{
    /// <summary>
    /// Provides some utility functions for items. 
    /// </summary>
    public static class ItemUtils
    {
        // this stuff to get all types that inherit from IItemFunction is only used in the editor, so we only want to do this in the editor
        // it is used to select the item function when creating an item in the inspector
#if UNITY_EDITOR
        /// <summary>
        /// All types that inherit from IItemFunction
        /// </summary>
        public static readonly List<Type> ItemFunctionTypes;

        static ItemUtils()
        {
            // find all types that inherit from IItemFunction. only search in the assembly that contains the ItemUtils class
            ItemFunctionTypes = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsInterface || type.IsAbstract)
                        continue;

                    if (type.GetInterfaces().Contains(typeof(IItemFunction)))
                    {
                        ItemFunctionTypes.Add(type);
                    }
                }
            }
        }

#endif
    }
}