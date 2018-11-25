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
    public Animator _selfAnim;
    public NavMeshAgent _nav;
    public CapsuleCollider _selfCollider;

    public MobType _mobtype;
    
    /// <summary>
    /// <para>Called to kill the entity</para>
    /// </summary>
    public void EnemyDie()
    {
        _nav.isStopped = true;
        _gm.EnemyDeath(_goldOnDeath, _scoreOnDeath);
        _selfAnim.SetBool("Dead", true);
        Destroy(gameObject, 1.5f);
    }

    /// <summary>
    /// <para>Called to make entity take damage</para>
    /// </summary>
    /// <param name="amount">how much damage to take (raw)</param>
    public void HealthTakeDamage(float amount)
    {
        _health -= amount * (1 - ((_armor / 100) * .5f));

        if (_health <= 0)
        {
            EnemyDie();
        }
    }

    /// <summary>
    /// <para>Called to heal entity</para>
    /// </summary>
    /// <param name="amount"></param>
    public void HealthHeal(float amount)
    {
        _health += amount;
    }

    private void Start()
    {
        _maxHealth = _health;
        _selfAnim = gameObject.GetComponent<Animator>();
        _nav = gameObject.GetComponent<NavMeshAgent>();
        _nav.speed = _speed;
        _gm = GameObject.Find("GameManager").GetComponent<gm_Primary>();
    }
}
