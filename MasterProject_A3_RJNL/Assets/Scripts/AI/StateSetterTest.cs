using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowUprising.AI;

public class StateSetterTest : MonoBehaviour
{
    public AIState currentState;
    public bool scuffed = true;

    private void Update()
    {
        if (scuffed)
            GetComponent<GuardState>().SetState(currentState);
    }

}
