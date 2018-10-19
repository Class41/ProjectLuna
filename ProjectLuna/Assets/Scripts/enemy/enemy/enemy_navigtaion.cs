﻿using UnityEngine;
using UnityEngine.AI;

public class enemy_navigtaion : MonoBehaviour
{

    public Transform _goal;
    public Transform _parent;

    public Animator _goalAnim;

    NavMeshAgent _agent;
    public float _stopRadius = 1.0f,
                 _entityDeathTime = .25f;

    bool _enemyDeathCalled = false;

    public GameObject _ui;
    public gm_Primary _gm;

    void Start()
    {
        _goal = GameObject.Find("player_01").transform;
        _goalAnim = GameObject.Find("player_01").GetComponent<Animator>();
        _parent = GameObject.FindGameObjectWithTag("epandprefab").transform;
        _gm = GameObject.Find("GameManager").GetComponent<gm_Primary>();
        this.transform.parent = _parent;
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _goal.position;
    }

    //currently, this is a distance-based system. SWITCH TO COLLISION BASED AFTER WE ADD MODELS & ANIMATIONS!
    private void FixedUpdate()
    {
        if (Vector3.Distance(_goal.position, this.gameObject.transform.position) < _stopRadius)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<CapsuleCollider>());

            if (!_enemyDeathCalled)
            {
                _gm.EnemyDeath(50, 50);
                _enemyDeathCalled = true;
            }
            Destroy(gameObject, .25f);
        }
        else if(_goalAnim.GetBool("movekeydown"))
        {
            _agent.destination = _goal.position;
        }
    }
}
