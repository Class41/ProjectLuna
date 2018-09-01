using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_follow_smooth : MonoBehaviour
{

    public Transform _target;
    public Vector3 _cameraOffset;
    public float _smoothSpeed = 0.125f,
                 max_x,
                 min_x,
                 max_y,
                 min_y,
                 max_z,
                 min_z;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 comboPos = _target.position + _cameraOffset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, comboPos, _smoothSpeed);
        Vector3 clampedSmoothedPos = new Vector3(
                                                    Mathf.Clamp(smoothedPos.x, min_x, max_x),
                                                    Mathf.Clamp(smoothedPos.y, min_y, max_y),
                                                    Mathf.Clamp(smoothedPos.z, min_z, max_z)
                                                 );
        transform.position = clampedSmoothedPos;
    }
}
