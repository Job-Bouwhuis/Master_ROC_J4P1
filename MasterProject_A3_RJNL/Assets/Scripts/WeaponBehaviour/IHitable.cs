// Creator: Ruben
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.WeaponBehaviour
{
    public interface IHitable
    {
        /// <summary>
        /// Event that is called when an object is hit by the players gun
        /// </summary>
        public void HitEvent();
    }
}
