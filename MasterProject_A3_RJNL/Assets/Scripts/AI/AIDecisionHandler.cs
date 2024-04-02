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
            GetComponent<AIPlayerConeDetector>().onObjectDetected += OnObjectDetected;
            buttons = FindObjectOfType<AlarmContainer>().alarmButtons;
        }

        private void OnObjectDetected(GameObject gObject)
        {
            if (!decisionMade)
            {
                if (gObject.tag == "Player")
                    MakeDecision(gObject.transform.position);
                else
                    GetComponent<GuardState>().SetState(AIState.SoundingAlarm);

                decisionMade = true;
            }


        }

        public void MakeDecision(Vector3 postition)
        {

                var distanceToPlayer = Vector3.Distance(postition, transform.position);
                foreach (var item in buttons)
                {
                    var distanceToButton = Vector3.Distance(item.transform.position, transform.position);
                    

                    if (distanceToButton < distanceToPlayer)
                    {   
                        GetComponent<GuardState>().SetState(AIState.SoundingAlarm);
                        decisionMade = true;
                        return;
                    }
                }
                GetComponent<GuardState>().SetState(AIState.Attacking);
                decisionMade = true;
            
        }

    }
}