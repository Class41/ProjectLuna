using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_stats_base : MonoBehaviour {

    public float health,
                 armor,
                 speed;

    private void Start()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
    }
}
