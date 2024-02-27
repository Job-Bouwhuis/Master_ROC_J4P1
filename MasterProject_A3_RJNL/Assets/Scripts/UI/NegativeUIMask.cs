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
}
