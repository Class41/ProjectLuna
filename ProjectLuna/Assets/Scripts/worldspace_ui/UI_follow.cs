using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_follow : MonoBehaviour {

    public Transform _parentObject;
    public Transform _camera;
    public Quaternion _UIRotation;

    private void Start()
    {
        _camera = GameObject.Find("Main Camera").transform;
       transform.LookAt(new Vector3(_camera.position.x, _camera.position.y * -1, _camera.position.z * -1));
        _UIRotation = transform.rotation;
    }

    void LateUpdate () {
        if (transform.position != null && _parentObject != null)
        {
            transform.position = _parentObject.position + new Vector3(0, 3, 0);
        }

        if(_parentObject == null)
        {
            Destroy(gameObject);
        }
	}
}
