using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class cam_healthfx : MonoBehaviour
{
    public PostProcessVolume _pfx;
    public player_health _pHealth;

    ChromaticAberration _chroma;

    void Start()
    {
        _pfx.profile.TryGetSettings(out _chroma);
    }

    private void Update()
    {
        _chroma.intensity.value = 1 - (_pHealth._health / _pHealth._maxHealth);
    }
}
