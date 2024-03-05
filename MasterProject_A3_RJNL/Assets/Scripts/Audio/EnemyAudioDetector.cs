using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioDetector : MonoBehaviour
{
    public float hearDistance = 10f;

    void Start()
    {
        AudioManager.Instance.OnPlayerSoundPlayed += OnPlayerSoundPlayed;
    }

    private void OnPlayerSoundPlayed(AudioManager.AudioEventArgs obj)
    {
        // detemian distane between player and me
        float distance = Vector3.Distance(obj.Position, transform.position);
        // if distance is smaller than hearDistnace
        if(distance < hearDistance)
        {
            // go to position that heard form
        }

        // go to position that heard form
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, hearDistance);
    }
}
