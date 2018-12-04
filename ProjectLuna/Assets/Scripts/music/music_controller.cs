/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: To control game audio in main scene
 *  
 */

using UnityEngine;
using UnityEngine.UI;

public class music_controller : MonoBehaviour
{
    public AudioSource _musicPrimary;
    public Animator _shopAnim;

    public AudioSource _healthSounds;
    public player_health _playerStats;

    public float _soundVolumeMult, _soundPrimaryMusicVolume = 1.0f;
    public Slider _volumeSlider;

    /// <summary>
    /// <para>Called by UI to change max volume</para>
    /// </summary>
    public void SetMult(float val)
    {
        _soundVolumeMult = val;
        PlayerPrefs.SetFloat("volume", val);
    }

    void Start()
    {
        _soundVolumeMult = PlayerPrefs.GetFloat("volume");
        _musicPrimary.time = Random.Range(0.0f, _musicPrimary.clip.length);
        _volumeSlider.value = _soundVolumeMult;
    }

    void Update()
    {
        bool shopstat = (_shopAnim.GetBool("shopopen")) ? true : false;
        if (_musicPrimary.volume != _soundPrimaryMusicVolume * _soundVolumeMult && !shopstat)
            _musicPrimary.volume = Mathf.Lerp(_musicPrimary.volume, _soundPrimaryMusicVolume * _soundVolumeMult, .2f * Time.deltaTime);
        else if (shopstat)
            _musicPrimary.volume = _soundPrimaryMusicVolume * .3f * _soundVolumeMult;

        if (_playerStats._health <= 0)
        {
            _musicPrimary.volume = _soundPrimaryMusicVolume * .2f * _soundVolumeMult;
            _healthSounds.volume = .3f * _soundVolumeMult;
        }

        _healthSounds.volume = (1.0f - (_playerStats._health / _playerStats._maxHealth)) * _soundVolumeMult;
    }
}
