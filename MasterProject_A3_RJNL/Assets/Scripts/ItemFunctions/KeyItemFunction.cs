// Under development.

using ShadowUprising.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyItemFunction : MonoBehaviour, IItemFunction
{
    public TMP_Text text;
    int i = 0;

    public void UseItem()
    {
        i++;
        text.text = "Key used " + i + " times.";
    }
}
