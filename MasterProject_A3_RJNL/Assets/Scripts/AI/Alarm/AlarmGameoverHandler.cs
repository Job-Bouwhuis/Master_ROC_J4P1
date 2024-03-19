using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShadowUprising.AI.Alarm
{
    public class AlarmGameoverHandler : MonoBehaviour
    {
        public void ActivateAlarm()
        {
            GameOver.GameOverManager.Instance.ShowGameOver();
        }
    }
}