using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowUprising.AI;

public class StateSetterTest : MonoBehaviour
{
    public AIState currentState;

    [Header("DEBUG")]
    [SerializeField] private bool lockState = false;

    private void Update()
    {
        if (lockState)
            GetComponent<GuardState>().SetState(currentState);
    }

}
