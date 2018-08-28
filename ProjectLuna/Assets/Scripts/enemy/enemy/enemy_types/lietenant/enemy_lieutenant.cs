using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_lieutenant : enemy_stats_base {

    public void OnTriggerEnter(Collider other)
    {
        if(other.name == "Bullet_01")
        {
            //TODO play some animation specific to the bullet maybe
            Destroy(gameObject);
        }
    }

}
