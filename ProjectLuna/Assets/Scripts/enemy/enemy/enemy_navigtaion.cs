/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls enemy movement and movement logic
 *  
 */

using UnityEngine;
using UnityEngine.AI;

public class enemy_navigtaion : MonoBehaviour
{

    public Transform _goal;
    public Transform _parent;

    public Animator _goalAnim;
    public Animator _selfAnim;

    NavMeshAgent _agent;
    private float _stopRadius = 3.0f,
                 _entityDeathTime = .25f;

    public GameObject _ui;
    public gm_Primary _gm;
    enemy_stats_base _selfStatus;

    void Start()
    {
        _goal = GameObject.Find("player_01").transform;
        _goalAnim = GameObject.Find("player_01").GetComponent<Animator>();
        //_selfAnim = gameObject.GetComponent<Animator>();
        _parent = GameObject.FindGameObjectWithTag("epandprefab").transform;
        _gm = GameObject.Find("GameManager").GetComponent<gm_Primary>();
        _selfStatus = gameObject.GetComponent<enemy_stats_base>();
        this.transform.parent = _parent;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
      //  bool walking = (true) ? true : false;
      //  _selfAnim.SetBool("Moving", walking);
    }

    private void FixedUpdate()
    {
        if (_selfStatus._health <= 0)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<CapsuleCollider>());
        }
        else if(_goalAnim.GetBool("movekeydown"))
        {
            _agent.destination = _goal.position;
        }
    }
}
