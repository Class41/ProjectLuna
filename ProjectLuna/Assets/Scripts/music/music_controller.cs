using UnityEngine;

public class music_controller : MonoBehaviour
{
    public AudioSource _musicPrimary;
    public Animator _shopAnim;

    public AudioSource _healthSounds;
    public player_health _playerStats;



    void Start()
    {
        _musicPrimary.time = Random.Range(0.0f, _musicPrimary.clip.length);
    }

    void Update()
    {
        bool shopstat = (_shopAnim.GetBool("shopopen")) ? true : false;
        if (_musicPrimary.volume < 1.0f && !shopstat)
            _musicPrimary.volume = Mathf.Lerp(_musicPrimary.volume, 1.0f, .2f * Time.deltaTime);
        else if (shopstat)
            _musicPrimary.volume = .3f;

        if (_playerStats._health <= 0)
        {
            _musicPrimary.volume = .1f;
            _healthSounds.volume = .3f;
        }

        _healthSounds.volume = 1.0f - (_playerStats._health / _playerStats._maxHealth);
    }
}
