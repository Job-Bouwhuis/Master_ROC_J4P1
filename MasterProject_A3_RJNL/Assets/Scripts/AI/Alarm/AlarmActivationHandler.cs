using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ShadowUprising.AI.Alarm
{
    public class AlarmActivationHandler : MonoBehaviour
    {
        bool activated;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<AIDistanceChecker>().onAIWithinRange += AIWithinRange;
        }

        private void AIWithinRange(GameObject obj)
        {
            if (!activated)
            {
                obj.GetComponent<NavMeshAgent>().enabled = false;
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                obj.GetComponent<Animator>().SetTrigger("PushingButton");
                var courotine = StartAlarm(2, obj);
                StartCoroutine(courotine);

                activated = true;
            }

        }


        private IEnumerator StartAlarm(float waitTime, GameObject gObject)
        {
            yield return new WaitForSeconds(waitTime);
            if (gObject != null)
                GameOver.GameOverManager.Instance.GameOver("The alarm has been activated!");
        }


    } 
}
