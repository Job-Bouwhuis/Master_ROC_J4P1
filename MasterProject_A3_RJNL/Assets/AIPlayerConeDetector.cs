//Creator: Luke
using System;
using UnityEngine;

public class AIPlayerConeDetector : MonoBehaviour
{
    /// <summary>
    /// is called when the player is detected
    /// </summary>
    public Action<Vector3> onPlayerDetected = delegate { };
    Transform playerTransform;


    private void Start()
    {
        Asign();
    }

    void Asign()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectPlayer())
            OnPlayerDetected(playerTransform.position);
    }

    bool DetectPlayer()
    {
        var heading = (playerTransform.position - transform.position).normalized;
        if (Vector3.Angle(heading, transform.forward) < 70)
        {
            Physics.Raycast(new Ray(transform.position, heading), out RaycastHit hitinfo, 50);
            if (hitinfo.transform.tag == "Player")
                return true;
        }
        return false;
    }

    void OnPlayerDetected(Vector3 playerPos)
    {
        onPlayerDetected.Invoke(playerPos);
    }

}
