using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_navigtaion : MonoBehaviour
{

    public Transform _goal;
    public Transform _parent;
    NavMeshAgent _agent;
    public float _stopRadius = 1.0f,
                 _entityDeathTime = .25f;

    bool enemyDeathCalled = false;

    public GameObject ui;
    public gm_Primary gm;

    void Start()
    {
        _goal = GameObject.Find("player_01").transform;
        _parent = GameObject.FindGameObjectWithTag("epandprefab").transform;
        gm = GameObject.Find("GameManager").GetComponent <gm_Primary>();
        this.transform.parent = _parent;
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _goal.position;
    }

    private void Update()
    {
        //THIS IS WHERE I HAVE THE DESTRUCTION HAPPEN. CHANGE THIS WHEN WE ACTUALLY HAVE ANIMATIONS INSTALLED
        if (Vector3.Distance(_goal.position, this.gameObject.transform.position) < _stopRadius)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<CapsuleCollider>());

            if (!enemyDeathCalled)
            {
                gm.EnemyDeath(50, 50);
                enemyDeathCalled = true;
            }
            Destroy(gameObject, .25f);
        }
        else
        {
            _agent.destination = _goal.position;
        }
    }
}
