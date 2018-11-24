/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Player health and damage handler
 *  
 */

using UnityEngine;

public class player_health : MonoBehaviour
{

    public float _health,
                 _maxHealth,
                 _armor;

    public float _baseHP,
                 _baseArmor;

    // Use this for initialization
    void Start()
    {
        CalculateStats();
        _health = _maxHealth;
    }

    public void CalculateStats()
    {
        _maxHealth = PlayerPrefs.GetInt("healthlevel") * 50 + _baseHP;
        _armor = PlayerPrefs.GetInt("armorlevel") * .5f + _baseArmor;
    }

    /// <summary>
    /// <para>Call when player needs to take damage (raw)</para>
    /// </summary>
    /// <param name="amount"></param>
    public void HealthTakeDamage(float amount)
    {
        _health -= (amount * (1 - (_armor / 100.0f)));

        if (_health <= 0)
        {
            //Todo: functionality for player death etc
        }
    }

    /// <summary>
    /// <para>Call when player needs to heal</para>
    /// </summary>
    /// <param name="amount"></param>
    public void HealthHeal(float amount)
    {
        _health += amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: Replace with actual useful damage stuff
        if (collision.gameObject.CompareTag("enemy"))
        {
            HealthTakeDamage(150);
        }
    }
}
