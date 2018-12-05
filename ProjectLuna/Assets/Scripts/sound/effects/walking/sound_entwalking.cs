using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_entwalking : MonoBehaviour
{
    public List<AudioClip> _walkClips = new List<AudioClip>();
    public AudioSource _walkPlayer;

    public void PlaySoundStep()
    {
        _walkPlayer.clip = _walkClips[Random.Range(0, _walkClips.Count - 1)];
        _walkPlayer.Play();
    }
}
