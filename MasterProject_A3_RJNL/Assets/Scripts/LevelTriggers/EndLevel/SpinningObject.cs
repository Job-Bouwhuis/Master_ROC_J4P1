//Creator: Ruben
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class makes its parent gameobject rotate on the Y value with a speed of rotationSpeed
/// </summary>
namespace ShadowUprising.LevelTriggers.EndLevel
{
    public class SpinningObject : MonoBehaviour
    {
        [SerializeField] float rotationSpeed;
        void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * 360 * Time.deltaTime, Space.World);
        }
    }
}