//Creator: Ruben
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.SecurityCamera
{
    public class CameraWaypoints : MonoBehaviour
    {
        [Tooltip("A list of transforms that act as waypoints for the camera's rotation. Only the rotation value of these transforms are used")]
        [SerializeField] List<Transform> waypoints;
        [Tooltip("The speed at which the camera rotates to the next waypoint")]
        [SerializeField] float speed;
        [Tooltip("The amount of time the camera will stay on a waypoint before going to the next in seconds")]
        [SerializeField] float timeoutDuration;
        Transform currentWaypoint;
        Transform nextWaypoint;
        int waypointIndex;
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