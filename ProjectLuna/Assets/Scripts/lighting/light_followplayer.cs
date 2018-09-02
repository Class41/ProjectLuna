using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_followplayer : MonoBehaviour
{

    public Transform player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.position.x, Mathf.Lerp(gameObject.transform.position.y ,(player.position.y + 4), .1f), player.position.z - 2);
    }
}
