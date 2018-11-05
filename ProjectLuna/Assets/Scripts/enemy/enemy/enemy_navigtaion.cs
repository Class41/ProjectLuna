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

    public Transform _goalTransform,
                     _parentTransform,
                     _selfTransform;

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
        _goalTransform = GameObject.Find("player_01").transform;
        _selfTransform = gameObject.GetComponent<Transform>();
        _goalAnim = GameObject.Find("player_01").GetComponent<Animator>();
        _selfAnim = gameObject.GetComponent<Animator>();
        _parentTransform = GameObject.FindGameObjectWithTag("epandprefab").transform;
        _gm = GameObject.Find("GameManager").GetComponent<gm_Primary>();
        _selfStatus = gameObject.GetComponent<enemy_stats_base>();
        this.transform.parent = _parentTransform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
      bool walking = (_agent.velocity.magnitude > .5) ? true : false;
      _selfAnim.SetBool("Moving", walking);

        if (!walking)
            _selfTransform.LookAt(_goalTransform);
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
            _agent.destination = _goalTransform.position;
        }
    }
}
