using ShadowUprising.AI.Alarm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShadowUprising.AI
{
    public class AIAlarmHandler : MonoBehaviour
    {

        List<GameObject> buttons;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<GuardState>().onStateChanged += GuardStateChange;
            var alarmContainer = FindObjectOfType<AlarmContainer>();
            if (alarmContainer != null)
                buttons = alarmContainer.alarmButtons;
        }

        void GuardStateChange(AIState state)
        {
            if (state == AIState.SoundingAlarm)
                GoToClosestAlarm();
        }

        void GoToClosestAlarm()
        {
            GameObject closestButton = null;
            foreach (var item in buttons)
            {
                if (closestButton == null)
                    closestButton = item;
                else if (Vector3.Distance(item.transform.position, transform.position) < Vector3.Distance(closestButton.transform.position, transform.position))
                {
                    closestButton = item;
                }
            }

            var comp = closestButton.GetComponentInChildren<AIDistanceChecker>();
            comp.AddAI(gameObject);
            GetComponent<AINavigationSystem>().SetCurrentWayPoint(comp.transform.position);
        }

    }
}