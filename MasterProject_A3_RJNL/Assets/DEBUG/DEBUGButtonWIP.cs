using ShadowUprising.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WinterRose;

public class DEBUGButtonWIP : ButtonFunction
{
    public override void Invoke(TextButton button)
    {
        Windows.MessageBox(
            text: "The function for this button is still in development. Come back in later versions to see its magic!", 
            title: "Attention", 
            buttons: Windows.MessageBoxButtons.OK, 
            icon: Windows.MessageBoxIcon.Information); 
    }
}
