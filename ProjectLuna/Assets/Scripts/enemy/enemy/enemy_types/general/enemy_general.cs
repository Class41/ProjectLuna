using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_general : enemy_stats_base {

    private enemy_navigtaion nav;

    public void OnTriggerEnter(Collider other)
    {
        nav = gameObject.GetComponent<enemy_navigtaion>();

        if(other.name == "Bullet_01")
        {

        }
    }

}
