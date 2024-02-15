using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigationSystem : MonoBehaviour
{
    NavMeshAgent navMesh;  
    Vector3 currentGoToPos;

    // Start is called before the first frame update
    void Start()
    {
        Asign();
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.SetDestination(currentGoToPos);
    }

    void Asign()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    public void SetCurrentWayPoint(Vector3 pos)
    {
        currentGoToPos = pos;
    }

}
