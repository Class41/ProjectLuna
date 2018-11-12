/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls the weapon trails to the animation
 *  
 */

using UnityEngine;

public class enemy_lieutenant_trailcontrol : MonoBehaviour
{
    public TrailRenderer _weaponTrail;

    /// <summary>
    /// <para>Sets the trail to enabled</para>
    /// </summary>
    public void SetTrailEnabled()
    {
        _weaponTrail.enabled = true;
    }

    /// <summary>
    /// <para>Sets the trail to disabled</para>
    /// </summary>
    public void SetTrailDisabled()
    {
        _weaponTrail.enabled = false;
    }
}
