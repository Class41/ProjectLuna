using UnityEngine;

public class UI_follow : MonoBehaviour
{

    public Transform _parentObject;
    public Transform _camera;
    public Quaternion _UIRotation;
    public float _smoothSpeed = 2f;

    private void Start()
    {
        _camera = GameObject.Find("Main Camera").transform;

        transform.rotation = _UIRotation;
    }

    void LateUpdate()
    {
        if (_parentObject != null)
        {
            Vector3 comboPos = _parentObject.position + new Vector3(0, 3, 0);
            Vector3 smoothedPos = Vector3.Lerp(transform.position, comboPos, _smoothSpeed);
            transform.position = smoothedPos;
        }

        if (_parentObject == null)
        {
            Destroy(gameObject);
        }
    }
}
