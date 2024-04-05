//Creator: Luke
//Edited: Ruben
using UnityEngine;
using ShadowUprising.Utils;

namespace ShadowUprising.AI
{
    /// <summary>
    /// handles the attacking for the ai
    /// </summary>
    public class AIAttackHandler : MonoBehaviour
    {

        /// <summary>
        /// the distance to the last player location until the ai can go back to roaming
        /// </summary>
        public float distanceToLastPlayerLoc;

        /// <summary>
        /// distance until the ai stops
        /// </summary>
        public float stopDistance;

        GuardState state;
        AINavigationSystem aiSystem;
        Timer timer = new Timer(3000);
        Vector3 lastPlayerLoc;

    // Start is called before the first frame update
        void Start()
        {
            Asign();
        }

        private void Update()
        {
            timer.Update(Time.deltaTime);
            CheckForSoundAlarm();
        }

        void Asign()
        {
            GetComponent<AIPlayerConeDetector>().onObjectDetected += OnObjectDetected;
            state = GetComponent<GuardState>();
            aiSystem = GetComponent<AINavigationSystem>();
            timer.elapsed += CallAlarmFunction;
        }

        void CallAlarmFunction()
        {
            GetComponent<GuardState>().SetState(AIState.SoundingAlarm);
        }

        void CheckForSoundAlarm()
        {
            if (IsAIAtLocation())
                SoundAlarm();
        }

        bool IsAIAtLocation()
        {
            if (Vector3.Distance(lastPlayerLoc, transform.position) < distanceToLastPlayerLoc)
                return true;
            return false;
        }

        void SoundAlarm()
        {
            if (state.CurrentState == AIState.Attacking)
                timer.StartTimer();
        }

        void OnObjectDetected(GameObject gameObject)
        {
            if (state.CurrentState == AIState.Attacking)
            {
                if (Vector3.Distance(transform.position, gameObject.transform.position) < stopDistance)
                {
                    aiSystem.SetCurrentWayPoint(transform.position);

                    Vector3 targetPostition = new Vector3(gameObject.transform.position.x,
                                       this.transform.position.y,
                                       gameObject.transform.position.z);
                    this.transform.LookAt(targetPostition);

                }
                else
                {
                    lastPlayerLoc = gameObject.transform.position;
                    aiSystem.SetCurrentWayPoint(gameObject.transform.position);
                    timer.ZeroTimer();
                }


            }
        }

        void StandingCondition(GameObject gameObject)
        {

                if (Vector3.Distance(transform.position, gameObject.transform.position) < stopDistance)
                {
                    aiSystem.SetCurrentWayPoint(Vector3.zero);
                    transform.LookAt(gameObject.transform.position, Vector3.right);
                    return;
                }
            
        }


    }
}