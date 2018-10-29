/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Torch test object
 *  
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ent_torch : world_entitybase {

    // Use this for initialization
    void Start()
    {
        EntitySpawningDistanceRestrictCheck();
    }
}
