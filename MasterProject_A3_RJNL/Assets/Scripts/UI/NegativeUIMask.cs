using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NegativeUIMask : Image
{
    public override Material materialForRendering
    {
        get
        {
            Material mat = new Material(base.materialForRendering);
            mat.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.NotEqual);
            return mat;
        }
    }

    protected override void Awake()
    {
        base.Awake();


        // ok bare with me, this is a bit of a hack. it seems that the inverted mask is not working properly unless the color is changed.
        // therefore i made this little hack to change to color to something else and then back to the original color.

        Color col = color;
        Color col2 = col;
        col2.a = 0.5f;
        color = col2;

        col2.a = col.a;

        color = col2;
    }
}
