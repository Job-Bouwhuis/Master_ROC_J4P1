using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To be deleted once the actual player script is implemented by Ruben and merged with the ingame UI branch.
/// </summary>
public class DebugPlayerHealth : MonoBehaviour
{
    public int health;
    public int stamina;

    public int maxHealth = 500;
    public int maxStamina = 500;

    private void Awake()
    {
        health = maxHealth;
        stamina = maxStamina;
    }
}
