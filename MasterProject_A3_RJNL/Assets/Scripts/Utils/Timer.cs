//Creator: Luke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ShadowUprising.Utils
{
    public class Timer
    {
        bool canCount;

        float elapsedMS;
        float setMS;

        public Action elapsed;

        bool timeElapsed;

        /// <summary>
        /// creates a timer based on a set ms
        /// </summary>
        /// <param name="setMS"> the time it takes before the event is called </param>
        public Timer(float setMS)
        {
            this.setMS = setMS / 1000;
           
        }

        // Update is called once per frame
        public void Update(float deltaTime)
        {
            if(!timeElapsed)
                Count(deltaTime);
        }

        void Count(float deltaTime)
        {
            if (canCount)
                elapsedMS += deltaTime;

            if (elapsedMS >= setMS)
            {
                Debug.Log("Invoked");
                elapsed.Invoke();
                timeElapsed = true;
            }

        }

        public void StopTimer()
        {
            canCount = false;
        }

        public void Restart()
        {
            elapsedMS = 0;
            canCount = true;
        }

        public void StartTimer()
        {
            canCount = true;
        }

    }
}