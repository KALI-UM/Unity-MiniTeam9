using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipPackData", menuName = "ScriptableObjects/AudioClipPackData")]
public class AudioClipPackData : ScriptableObject
{
    public AudioClip[] bgms;
    public AudioClip[] sfxs;

    
}
