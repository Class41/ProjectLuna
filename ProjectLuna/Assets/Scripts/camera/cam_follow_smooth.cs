/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Smooths camera movement while following player and leads player's movements.
 *  
 */

using UnityEngine;

public class cam_follow_smooth : MonoBehaviour
{

    public Transform _target;
    public Rigidbody _targetRigidBody;
    public Vector3 _cameraOffset;
    public float _smoothSpeed = 0.125f,
                 _velocitySensitivity = 0.75f,
                 _max_x,
                 _min_x,
                 _max_y,
                 _min_y,
                 _max_z,
                 _min_z;

    private void Start()
    {
        _targetRigidBody = _target.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// <para>Shakes camera on player attack</para>
    /// </summary>
    public void ShakeCamera()
    {
        transform.Translate(new Vector3(0, Random.Range(-.25f, 0), 0), Space.Self);
    }

    //smooth camera movement when following player
    void FixedUpdate()
    {
        Vector3 comboPos = _target.position + _cameraOffset + (_velocitySensitivity * new Vector3(_targetRigidBody.velocity.x, 0, _targetRigidBody.velocity.z));
        Vector3 smoothedPos = Vector3.Lerp(transform.position, comboPos, _smoothSpeed);
        Vector3 clampedSmoothedPos = new Vector3(
                                                    Mathf.Clamp(smoothedPos.x, _min_x, _max_x),
                                                    Mathf.Clamp(smoothedPos.y, _min_y, _max_y),
                                                    Mathf.Clamp(smoothedPos.z, _min_z, _max_z)
                                                 );
        transform.position = clampedSmoothedPos;
    }
}
