using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_navigtaion : MonoBehaviour {

    public Transform goal;

    void Start()
    {
        goal = GameObject.Find("path_end").transform;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

        //if (goal.position.x - gameObject.transform.position.x  < 5)
       // {
         //   Destroy(gameObject.GetComponent<Rigidbody>());
        //    Destroy(gameObject.GetComponent<CapsuleCollider>());
        //    Destroy(gameObject.GetComponent<NavMeshAgent>());
       // }


        
    }
}
