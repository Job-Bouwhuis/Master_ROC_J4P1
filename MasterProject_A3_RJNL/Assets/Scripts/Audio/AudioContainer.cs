using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Audio Container", menuName = "Audio/Audio Container")]
public class AudioContainer : ScriptableObject
{
    public AudioClip audioClip;
    public AudioType audioType;
    public int loudnessValue;
}
