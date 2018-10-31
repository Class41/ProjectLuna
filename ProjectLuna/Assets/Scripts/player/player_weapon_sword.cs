using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_weapon_sword : MonoBehaviour
{
    BoxCollider _colliderStrikeClose;

    public void Start()
    {
        _colliderStrikeClose = gameObject.GetComponent<BoxCollider>();
    }

    public void EnableStrikeCloseCollider()
    {
        _colliderStrikeClose.enabled = true;
    }

    public void DisableStrikeCloseCollider()
    {
        _colliderStrikeClose.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I hit something!: " + other.tag);
    }
}
