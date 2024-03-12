//Creator: Ruben
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.SecurityCamera
{
    public class CameraWaypoints : MonoBehaviour
    {
        [SerializeField] List<Transform> waypoints;
        [SerializeField] float speed;
        [SerializeField] float timeoutDuration;
        int waypointIndex;
        Transform currentWaypoint;
        Transform nextWaypoint;
        float timer = 0.0f;
        bool timeout;

        private void Start()
        {
            SetWaypoints();
        }

        void Update()
        {
            if (timeout)
                UpdateTimeout();
            else
                UpdateLerp();
        }

        void SetWaypoints()
        {
            currentWaypoint = waypoints[waypointIndex];
            nextWaypoint = waypoints[(waypointIndex + 1) % waypoints.Count];
        }

        void UpdateLerp()
        {
            float lerpProgress = timer * speed;
            transform.localRotation = Quaternion.Lerp(currentWaypoint.localRotation, nextWaypoint.localRotation, lerpProgress);
            timer = timer + Time.deltaTime;
            if (lerpProgress >= 1)
                StartTimeout();
        }

        void StartTimeout()
        {
            timer = 0;
            timeout = true;
        }

        void EndTimeout()
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
            SetWaypoints();

            timer = 0;
            timeout = false;
        }

        void UpdateTimeout()
        {
            timer = timer + Time.deltaTime;
            if (timer >= timeoutDuration)
                EndTimeout();
        }
    }
}