using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_general : enemy_stats_base {

    private enemy_navigtaion nav;

    public void Start()
    {
        nav = gameObject.GetComponent<enemy_navigtaion>();
    }

    public void OnTriggerEnter(Collider other)
    {

        if(other.name == "Bullet_01")
        {

        }
    }
}
