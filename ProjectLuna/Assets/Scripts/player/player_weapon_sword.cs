/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls weaoon damage coming from player
 *  
 */

using UnityEngine;

public class player_weapon_sword : MonoBehaviour
{
    BoxCollider _colliderStrikeClose;

    public void Start()
    {
        _colliderStrikeClose = gameObject.GetComponent<BoxCollider>();
    }

    /// <summary>
    /// <para>Enable collider infront of player. Small range</para>
    /// </summary>
    public void EnableStrikeCloseCollider()
    {
        _colliderStrikeClose.enabled = true;
    }

    /// <summary>
    /// <para>Disable collider infront of player. Small range</para>
    /// </summary>
    public void DisableStrikeCloseCollider()
    {
        _colliderStrikeClose.enabled = false;
    }

    /// <summary>
    /// <para>Quickly flick on and off collider infront of player. Small range</para>
    /// </summary>
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
            other.GetComponent<enemy_knockback>().KnockbackThis(other.GetComponent<Transform>().position + gameObject.GetComponent<Transform>().forward * 20);
        }
    }
}
