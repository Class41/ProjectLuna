using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_navigtaion : MonoBehaviour {

    public Transform _goal;
    public Transform _parent;
    NavMeshAgent _agent;
    public float _stopRadius = 1.0f,
                 _entityDeathTime = .25f;

    void Start()
    {
        _goal = GameObject.Find("player_01").transform;
        _parent = GameObject.FindGameObjectWithTag("epandprefab").transform;
        this.transform.parent = _parent;
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _goal.position;
    }

    private void Update()
    {
        if (Vector3.Distance(_goal.position, this.gameObject.transform.position) < _stopRadius)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<CapsuleCollider>());

            Destroy(gameObject, .25f);
        }
        else
        {
            _agent.destination = _goal.position;
        }
    }
}
