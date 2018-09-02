using UnityEngine;

public class cam_follow_smooth : MonoBehaviour
{

    public Transform _target;
    public Vector3 _cameraOffset;
    public float _smoothSpeed = 0.125f, 
                 _max_x,
                 _min_x,
                 _max_y,
                 _min_y,
                 _max_z,
                 _min_z;

    //smooth camera movement when following player
    void FixedUpdate()
    {
        Vector3 comboPos = _target.position + _cameraOffset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, comboPos, _smoothSpeed);
        Vector3 clampedSmoothedPos = new Vector3(
                                                    Mathf.Clamp(smoothedPos.x, _min_x, _max_x),
                                                    Mathf.Clamp(smoothedPos.y, _min_y, _max_y),
                                                    Mathf.Clamp(smoothedPos.z, _min_z, _max_z)
                                                 );
        transform.position = clampedSmoothedPos;
    }
}
