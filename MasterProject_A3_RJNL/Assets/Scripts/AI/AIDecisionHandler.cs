using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.AI.Alarm
{
    /// <summary>
    /// Makes the decisions for the ai
    /// </summary>
    public class AIDecisionHandler : MonoBehaviour
    {

        bool decisionMade = false;
        List<GameObject> buttons;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<AIPlayerConeDetector>().onPlayerDetected += OnPlayerDetected;
            buttons = FindObjectOfType<AlarmContainer>().alarmButtons;
        }

        private void OnPlayerDetected(Vector3 postition)
        {
            MakeDecision(postition);
            
        }

        public void MakeDecision(Vector3 postition)
        {
            if (!decisionMade)
            {
                var distanceToPlayer = Vector3.Distance(postition, transform.position);
                foreach (var item in buttons)
                {
                    var distanceToButton = Vector3.Distance(item.transform.position, transform.position);
                    

                    if (distanceToButton < distanceToPlayer)
                    {   
                        GetComponent<GuardState>().SetState(AIState.SoundingAlarm);
                        var comp = item.GetComponentInChildren<AIDistanceChecker>();
                        comp.AddAI(gameObject);
                        GetComponent<AINavigationSystem>().SetCurrentWayPoint(comp.transform.position);
                        decisionMade = true;
                        return;
                    }
                }
                GetComponent<GuardState>().SetState(AIState.Attacking);
                Debug.Log("SetState");
                decisionMade = true;
            }
        }

        public void SoundClosestAlarm()
        {
            int index = 0;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Vector3.Distance(buttons[i].transform.position, transform.position) < Vector3.Distance(buttons[index].transform.position, transform.position))
                {
                    index = i;
                }
            }

            GetComponent<GuardState>().SetState(AIState.SoundingAlarm);
            var comp = buttons[index].GetComponentInChildren<AIDistanceChecker>();
            comp.AddAI(gameObject);
            GetComponent<AINavigationSystem>().SetCurrentWayPoint(comp.transform.position);
        }
    }
}