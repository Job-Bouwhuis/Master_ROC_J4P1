using ShadowUprising.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherItemFunction : IItemFunction
{
    public void UseItem()
    {
        Debug.Log("Another Item Used");
    }
}
