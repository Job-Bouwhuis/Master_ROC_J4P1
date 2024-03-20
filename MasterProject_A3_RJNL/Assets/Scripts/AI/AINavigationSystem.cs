//Creator: Luke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShadowUprising.AI
{
    /// <summary>
    /// ai navigation system handles eveything that has to do with navmesh
    /// </summary>
    public class AINavigationSystem : MonoBehaviour
    {
        NavMeshAgent navMesh;
        Vector3 currentGoToPos;


        /// <summary>
        /// sets the current go to postion
        /// </summary>
        /// <param name="pos">the postion to go to</param>
        public void SetCurrentWayPoint(Vector3 pos)
        {
            currentGoToPos = pos;
        }

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



    }
}

