// Creator: job

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.UnityUtils
{
    /// <summary>
    /// when making a <see cref="Singleton{T}"/> class, you can use this attribute to make the object be added to the <b>DontDestroyOnLoad</b> list
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DontDestroyOnLoadAttribute : Attribute
    {
    }
}