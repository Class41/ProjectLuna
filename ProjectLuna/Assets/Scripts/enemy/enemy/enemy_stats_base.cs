using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MobType
{
    ENEMY_PLEB,
    ENEMY_BOSS
}

public class enemy_stats_base : MonoBehaviour
{

    public float health,
                 maxHealth,
                 armor,
                 speed;

    public GameObject ui;

    public MobType mobtype;

    private void health_takeDamage(float amount)
    {
        health -= amount * (1 - ((armor / 100) * .5f));
    }

    private void health_heal(float amount)
    {
        health += amount;
    }

    private void Start()
    {
        maxHealth = health;
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
