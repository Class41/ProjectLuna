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

    public void AutoToggleCloseCollider()
    {
        EnableStrikeCloseCollider();
        DisableStrikeCloseCollider();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            enemy_stats_base enemyStats = other.GetComponent<enemy_stats_base>();

            enemyStats.HealthTakeDamage(750.0f);
        }
    }
}
