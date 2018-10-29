/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls line from UI to large enemy
 *  
 */

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
    
    //draw line between enemy and the UI it is associated with
    void LateUpdate()
    {
        if (_parentObject != null)
        {
            _line.SetPosition(0, gameObject.transform.position + _uiDisplacement);
            _line.SetPosition(1, _parentObject.transform.position + _parentDisplacement);
        }
    }
}
