using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_enemysound : MonoBehaviour
{
    public AudioSource _attackSource;

    public void PlayAttackSound()
    {
        _attackSource.Play();
    }
}
