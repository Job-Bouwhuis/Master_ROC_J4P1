using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimiationHandler : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        AsignComponents();
    }

    void OnAIMoving()
    {
        animator.SetTrigger("Moving");
    }

    void OnAIStanding()
    {
        animator.SetTrigger("Standing");
    }

    void AsignComponents()
    {
        animator = GetComponent<Animator>();
        var comp = GetComponent<AIMovementChecker>();
        comp.onAIMoving += OnAIMoving;
        comp.onAIStanding += OnAIStanding;
    }

}
