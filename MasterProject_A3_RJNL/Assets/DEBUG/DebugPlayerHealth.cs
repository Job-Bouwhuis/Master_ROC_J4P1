using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
