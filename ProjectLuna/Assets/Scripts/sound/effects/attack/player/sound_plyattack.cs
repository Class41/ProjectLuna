using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_plyattack : MonoBehaviour
{
    public AudioClip[] _plyAttackSounds = new AudioClip[3];
    public AudioSource _plyAttackSoundSource;

    public void PlayAttackSound(int index)
    {
        _plyAttackSoundSource.clip = _plyAttackSounds[index];
        _plyAttackSoundSource.Play();
    }
}
