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
        GuardState state;
        Utils.Timer timer = new Utils.Timer(3000);
        Action onBodySpotted = delegate { };

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<AIPlayerConeDetector>().onObjectDetected += OnObjectDetected;
            state = GetComponent<GuardState>();
            timer.elapsed += SetToRoam;
            var alarmContainer = FindObjectOfType<AlarmContainer>();
            if (alarmContainer != null)
                buttons = alarmContainer.alarmButtons;
        }

        private void Update()
        {
            timer.Update(Time.deltaTime);
        }

        void SetToRoam()
        {
            state.SetState(AIState.Roaming);
            decisionMade = false;
        }

        void DecisionWhenThereIsNoAlarm(GameObject gObject)
        {
            if (gObject.tag == "Player" && !decisionMade)
            {
                state.SetState(AIState.Attacking);
                Debug.Log("StateSet!");
                decisionMade = true;
            }

            if (gObject.tag == "Player")
                timer.Restart(); 
            return;
        }

        private void OnObjectDetected(GameObject gObject)
        {
            if (buttons == null)
                DecisionWhenThereIsNoAlarm(gObject);

            if (!decisionMade)
            {
                if (gObject.tag == "Player")
                    MakeDecision(gObject.transform.position);
                else
                {
                    state.SetState(AIState.SoundingAlarm);
                    onBodySpotted.Invoke();
                }

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
                        state.SetState(AIState.SoundingAlarm);
                        decisionMade = true;
                        return;
                    }
                }
                state.SetState(AIState.Attacking);
                decisionMade = true;
            
        }

    }
}