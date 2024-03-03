using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(VignetteHandler))]
public class DetectionIndicator : MonoBehaviour
{
    [Header("Functionality")]
    public bool isDetecting = false;
    public bool isDetected = false;

    [Header("Settings")]
    public float detectionSpeed = 0.1f;


    [Header("References - DO NOT EDIT")]
    [SerializeField] private VignetteHandler vignette;

    // Start is called before the first frame update
    void Start()
    {
        vignette = GetComponent<VignetteHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDetecting)
        {
            // detectionSpeed is in seconds. 1 / detectionSpeed is the number of frames it takes to go from 0 to 1
            vignette.TargetPercentage += Time.deltaTime * (1 / detectionSpeed);
            
            if(vignette.TargetPercentage >= 1.0f)
            {
                isDetecting = true;
                isDetected = true;
            }
        }
    }
}
