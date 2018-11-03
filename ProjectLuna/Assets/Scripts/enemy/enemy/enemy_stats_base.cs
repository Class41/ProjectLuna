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

   public int _scoreOnDeath,
              _goldOnDeath;

    public GameObject _ui;
    public gm_Primary _gm;

    public MobType _mobtype;

    public void EnemyDie()
    {
        _gm.EnemyDeath(_goldOnDeath, _scoreOnDeath);
        Destroy(gameObject, .25f);
    }

    public void HealthTakeDamage(float amount)
    {
        _health -= amount * (1 - ((_armor / 100) * .5f));

        if (_health <= 0)
        {
            EnemyDie();
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
        _gm = GameObject.Find("GameManager").GetComponent<gm_Primary>();
    }
}
