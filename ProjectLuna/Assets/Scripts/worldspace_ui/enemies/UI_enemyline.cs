using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_enemyline : MonoBehaviour
{

    public LineRenderer _line;
    public GameObject _parentObject;
    public Vector3 _uiDisplacement,
                   _parentDisplacement;

    public void Start()
    {
        _line = gameObject.GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        _line.SetPosition(0, gameObject.transform.position + _uiDisplacement);
        _line.SetPosition(1, _parentObject.transform.position + _parentDisplacement);
    }
}
