using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_playerdamage : MonoBehaviour
{
    BoxCollider _colliderStrikeClose;
    public float _damage;

    public void Start()
    {
        _colliderStrikeClose = gameObject.GetComponent<BoxCollider>();
    }

    /// <summary>
    /// <para>Enable collider infront of enemy. Small range</para>
    /// </summary>
    public void EnableStrikeCloseCollider()
    {
        _colliderStrikeClose.enabled = true;
    }

    /// <summary>
    /// <para>Disable collider infront of enemy. Small range</para>
    /// </summary>
    public void DisableStrikeCloseCollider()
    {
        _colliderStrikeClose.enabled = false;
    }

    /// <summary>
    /// <para>Quickly flick on and off collider infront of enemy. Small range</para>
    /// </summary>
    public void AutoToggleCloseCollider()
    {
        EnableStrikeCloseCollider();
        DisableStrikeCloseCollider();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            player_health playerStats = other.GetComponent<player_health>();

            playerStats.HealthTakeDamage(_damage);
        }
    }
}
