/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Enemy stats base class
 *  
 */

using UnityEngine;
using UnityEngine.AI;

public enum MobType
{
    ENEMY_PLEB,
    ENEMY_BOSS
}

public class enemy_stats_base : MonoBehaviour
{

    public float _health,
                 _maxHealth,
                 _armor,
                 _speed;

    public GameObject _ui;

    public MobType _mobtype;

    public void HealthTakeDamage(float amount)
    {
        Debug.Log("I've been hit for " + amount);
        _health -= amount * (1 - ((_armor / 100) * .5f));

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HealthHeal(float amount)
    {
        _health += amount;
    }

    private void Start()
    {
        _maxHealth = _health;
        gameObject.GetComponent<NavMeshAgent>().speed = _speed;
    }
}
