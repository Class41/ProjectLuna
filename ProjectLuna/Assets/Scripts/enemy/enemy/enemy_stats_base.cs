using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MobType
{
    ENEMY_PLEB,
    ENEMY_BOSS
}

public class enemy_stats_base : MonoBehaviour {

    public float health,
                 armor,
                 speed;

    public GameObject ui;

    public MobType mobtype;

    private void Start()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
    }
}
