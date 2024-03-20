using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMouseMover : MonoBehaviour
{
    private Vector3 mouseOrigin;
    private Vector3 rotationOrigin;
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            mouseOrigin = Input.mousePosition;
            rotationOrigin = transform.rotation.eulerAngles;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - mouseOrigin;
            float rotationSpeed = 0.1f;
            float deltaX = mouseDelta.x * rotationSpeed;
            float deltaY = -mouseDelta.y * rotationSpeed; // Invert y-axis for more intuitive control
            transform.rotation = Quaternion.Euler(rotationOrigin + new Vector3(deltaY, deltaX, 0f));
        }
    }
}
