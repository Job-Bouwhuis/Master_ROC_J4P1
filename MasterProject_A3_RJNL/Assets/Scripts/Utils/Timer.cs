//Creator: Luke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ShadowUprising.Utils
{
    /// <summary>
    /// requires <see cref="Timer.Update(float)"/> in update loop to works
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// is called when the set amount of time is elapsed
        /// </summary>
        public Action elapsed;

        float elapsedMS;
        float setMS;

        bool canCount;
        bool timeElapsed;

        /// <summary>
        /// creates a timer based on a set ms
        /// </summary>
        /// <param name="setMS"> the time it takes before the event is called </param>
        public Timer(float setMS)
        {
            this.setMS = setMS / 1000;
           
        }

        /// <summary>
        /// updates the timer his elapsed time
        /// </summary>
        /// <param name="deltaTime"> time.deltatime within unity </param>
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
                elapsed.Invoke();
                timeElapsed = true;
            }

        }


        /// <summary>
        /// stops the timer
        /// </summary>
        public void StopTimer()
        {
            canCount = false;
        }

        /// <summary>
        /// restarts the timer
        /// </summary>
        public void Restart()
        {
            elapsedMS = 0;
            canCount = true;
            timeElapsed = false;
        }

        /// <summary>
        /// starts the timer
        /// </summary>
        public void StartTimer()
        {
            canCount = true;
        }

        /// <summary>
        /// sets the elapsed seconds of the timer back to zero
        /// </summary>
        public void ZeroTimer()
        {
            elapsedMS = 0;
            timeElapsed = false;
        }

    }
}