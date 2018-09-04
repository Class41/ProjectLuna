using UnityEngine;
using System.Collections.Generic;

public class world_entitybase : MonoBehaviour
{
    public float _expiration,
                 _durability,
                 _distanceMaxFromCreator;

    public Vector3 _position;

    public Transform _creator;

    public List<string> acceptableSpawnTags = new List<string>();

    private void Start()
    {
        
    }

    void Update()
    {
        if (_expiration < Time.timeSinceLevelLoad || _durability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
