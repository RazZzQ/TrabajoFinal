using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "ScriptableObjects/AudioSettings", order = 1)]
public class SO_Audio : ScriptableObject
{
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public AudioClip[] sfxClips; // Lista de clips de audio para efectos de sonido
}
