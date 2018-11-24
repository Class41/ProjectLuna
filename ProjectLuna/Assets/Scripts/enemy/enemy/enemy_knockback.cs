/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls enemy knockback
 *  
 */

using UnityEngine;

public class enemy_knockback : MonoBehaviour
{
    private bool _knockedback;
    private Transform _self;
    private Animator _selfAnim;
    private Vector3 _destination;
    private float _time = .3f;
    private float _endTime;

    private void Start()
    {
        _self = gameObject.GetComponent<Transform>();
        _selfAnim = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// <para>Knockback target to this destination</para>
    /// </summary>
    /// <param name="pos"></param>
    public void KnockbackThis(Vector3 pos)
    {
        _destination = pos;
        _knockedback = true;
        _selfAnim.SetBool("Knockback", true);
        _endTime = Time.timeSinceLevelLoad + _time;
    }

    void FixedUpdate()
    {
        if (_knockedback)
        {
            _self.position = Vector3.Lerp(_self.position, _destination, _time * Time.deltaTime);

            if (Time.timeSinceLevelLoad - _endTime  > 0)
            {
                _knockedback = false;
                _selfAnim.SetBool("Knockback", false);
            }
        }
    }
}
