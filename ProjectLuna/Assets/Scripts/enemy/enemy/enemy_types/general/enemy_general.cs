/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: High level enemy specific behavior
 *  
 */

using UnityEngine;

public class enemy_general : enemy_stats_base
{

    private enemy_navigtaion _nav;

    public void Start()
    {
        _nav = gameObject.GetComponent<enemy_navigtaion>();
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.name == "Bullet_01")
        {

        }
    }
}
