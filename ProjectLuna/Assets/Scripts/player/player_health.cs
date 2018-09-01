using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_health : MonoBehaviour
{

    public float health,
                 max_health,
                 armor;

    // Use this for initialization
    void Start()
    {
        max_health = health;
    }

    private void health_takeDamage(float amount)
    {
        health -= (amount * (1 - (armor / 100.0f)));
    }

    private void health_heal(float amount)
    {
        health += amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: Replace with actual useful damage stuff
        if(collision.gameObject.name.Contains("enemy"))
        {
            health_takeDamage(150);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            //Todo: functionality for player death etc
        }
    }
}
