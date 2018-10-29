/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Keeps track of spawned world entities (point and click)
 *  
 */

using System.Collections.Generic;
using UnityEngine;

public class gm_EntityManager : MonoBehaviour
{

    public List<GameObject> activeEntities = new List<GameObject>();
    public List<GameObject> availableEntitiesList = new List<GameObject>();

    public GameObject _selectedEntity,
                      _player,
                      _entContainer;

    public Camera _currentCamera;

    public Vector3 _lastSelectedWorldPoint;

    void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selectedEntity = availableEntitiesList[0]; //FOR TESTING PURPOSES ONLY. REMOVE THIS LINE LATER!!!!!!!!!!!!!!!!!!!!!!!@@@@@@@

            if (_selectedEntity != null)
            {
                RaycastHit hit;
                Ray ray = _currentCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    _lastSelectedWorldPoint = hit.point;
                }

                spawnEntity(_selectedEntity, _player);
            }
        }
    }

    public void spawnEntity(GameObject ent, GameObject creator = null)
    {
        GameObject spawnedEnt = Instantiate(ent, _lastSelectedWorldPoint, ent.transform.rotation, _entContainer.transform) as GameObject;

        if (creator != null)
            spawnedEnt.GetComponent<world_entitybase>()._creator = creator.transform;

        _selectedEntity = null;

        activeEntities.Add(spawnedEnt);
    }
}
