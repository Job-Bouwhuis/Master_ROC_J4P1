using ShadowUprising.DeathSaves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGCreateSnapshot : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            DeathSaveManager.Instance.MakeSnapshot();
        }
    }
}
