/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Low level enemy behavior
 *  
 */

using UnityEngine;

public class enemy_infantry : enemy_stats_base
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet_01")
        {
            //TODO play some animation specific to the bullet maybe
            Destroy(gameObject);
        }
    }

}
