/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Base class for droppable/spawnable world entities
 *  
 */

using UnityEngine;
using System.Collections.Generic;

public class world_entitybase : MonoBehaviour
{
    public float _expiration = Mathf.Infinity,
                 _durability = 1,
                 _distanceMaxFromCreator = 1;

    public Transform _creator;

    public List<string> acceptableSpawnTags = new List<string>();

    protected void EntitySpawningDistanceRestrictCheck()
    {
        if(Vector3.Distance(_creator.position, gameObject.transform.position) > _distanceMaxFromCreator)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (_expiration < Time.timeSinceLevelLoad || _durability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
