using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSetterTest : MonoBehaviour
{
    public AIState currentState;
    private void Update()
    {
        GetComponent<GuardState>().SetState(currentState);
    }

}
