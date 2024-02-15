using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRoamingHandler : MonoBehaviour
{
    public List<Transform> roamingPoints;

    AINavigationSystem aiSystem;
    Vector3 currentGoTo;

    bool roaming;


    // Start is called before the first frame update
    void Start()
    {
       
        Asign();
    }

    // Update is called once per frame
    void Update()
    {
        if (roaming)
        {
            if (Vector3.Distance(currentGoTo, transform.position) < 1f)
                if (IsInvoking("SetNewGoToPos"))
                    Invoke("SetNewGoToPos", 3.5f);
        }
    }

    void SetNewGoToPos()
    {
        currentGoTo = roamingPoints[Random.RandomRange(0, roamingPoints.Count)].position;
        aiSystem.SetCurrentWayPoint(currentGoTo);
    }

    void SetState(AIState currentState)
    {
        roaming = currentState == AIState.Roaming;


        if (!roaming)
            if (IsInvoking("SetNewGoToPos"))
                CancelInvoke("SetNewGoToPos");
            else
                SetNewGoToPos();
    }

    void Asign()
    {
        currentGoTo = roamingPoints[Random.RandomRange(0, roamingPoints.Count)].position;
        GetComponent<GuardState>().onStateChanged += SetState;
        aiSystem = GetComponent<AINavigationSystem>();
    }

}
