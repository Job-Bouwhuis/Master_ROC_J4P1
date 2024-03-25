//Created by Niels
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.Audio
{
    /// <summary>
    /// enum space for audio types
    /// </summary>
    public enum AudioType
    {
        /// <summary>
        /// player walk SFX with a value of 2
        /// </summary>
        playerWalk = 2,
        /// <summary>
        /// player crouch SFX with a value of 1
        /// </summary>
        playerCrouch = 1,
        /// <summary>
        /// player sprint SFX with a value of 3
        /// </summary>
        playerSprint = 3,
        /// <summary>
        /// player shoot SFX with a value of 5
        /// </summary>
        playerShoot = 5,
        /// <summary>
        /// player reload SFX with a value of 1
        /// </summary>
        playerReload = 1,
        /// <summary>
        /// player heal SFX with a value of 1
        /// </summary>
        playerHeal = 1,
        /// <summary>
        /// player grab item SFX with a value of 1
        /// </summary>
        playerGrabItem = 1,
        /// <summary>
        /// player world interact with a value of 0
        /// </summary>
        playerWorldInteract = 0,
    }
}