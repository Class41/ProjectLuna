/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls player movement 
 *  
 */

using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float _forcemod, _attackAvailable, _attackCD;
    public Animator _anim;

    public Material _barrierMat;
    public float _barrierVisibility = 0,
                 _barrierDisappearTime = 0;

    public Rigidbody _plyRigidbody;
    public Transform _plyTranform;

    public bool _attacking;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("invisbarrierwall"))
        {
            _barrierVisibility = .25f;
        }
    }

    private void Start()
    {
        _plyRigidbody = gameObject.GetComponent<Rigidbody>();
        _plyTranform = gameObject.GetComponent<Transform>();
    }

    /// <summary>
    /// <para>This is called by the animation controller once the attack animation is finished.</para>
    /// </summary>
    public void SetNoAttack()
    {
        _anim.SetBool("attackdown", false);
        _attacking = false;
        _attackAvailable = Time.timeSinceLevelLoad + _attackCD;
    }

    void Update()
    {
        if (_barrierVisibility > 0)
        {
            _barrierMat.SetFloat("Vector1_B528FB43", _barrierVisibility);
            _barrierVisibility = Mathf.Lerp(_barrierVisibility, 0.0f, _barrierDisappearTime);
        }

        _anim.SetBool("movekeydown", false);

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !_attacking)
        {
            _plyRigidbody.AddForce(Vector3.forward * _forcemod * Time.deltaTime * 100, ForceMode.Force);
            if (_plyRigidbody.velocity.magnitude > 1)
                _anim.SetBool("movekeydown", true);
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !_attacking)
        {
            _plyRigidbody.AddForce(Vector3.back * _forcemod * Time.deltaTime * 100, ForceMode.Force);
            if (_plyRigidbody.velocity.magnitude > 1)
                _anim.SetBool("movekeydown", true);
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !_attacking)
        {
            _plyRigidbody.AddForce(Vector3.left * _forcemod * Time.deltaTime * 100, ForceMode.Force);
            if (_plyRigidbody.velocity.magnitude > 1)
                _anim.SetBool("movekeydown", true);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !_attacking)
        {
            _plyRigidbody.AddForce(Vector3.right * _forcemod * Time.deltaTime * 100, ForceMode.Force);
            if (_plyRigidbody.velocity.magnitude > 1)
                _anim.SetBool("movekeydown", true);
        }

        if (Input.GetKey(KeyCode.Space) && !_attacking && !_anim.GetBool("attackdown") && (_attackAvailable - Time.timeSinceLevelLoad) <= 0)
        {
            _anim.SetBool("attackdown", true);
            _attacking = true;
        }

        if (_plyRigidbody.velocity.magnitude > 1)
            _plyTranform.forward = Vector3.Slerp(_plyTranform.forward, new Vector3(_plyRigidbody.velocity.normalized.x, 0, _plyRigidbody.velocity.normalized.z), 10.0f * Time.deltaTime);

    }
}
