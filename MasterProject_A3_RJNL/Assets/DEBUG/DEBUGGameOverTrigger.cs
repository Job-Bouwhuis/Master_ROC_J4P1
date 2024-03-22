using ShadowUprising.GameOver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGGameOverTrigger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            GameOverManager.Instance.ShowGameOver();
        }
    }
}
 