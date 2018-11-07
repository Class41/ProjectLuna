/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls skybox spin
 *  
 */

using UnityEngine;

public class gm_Spinsky : MonoBehaviour
{
    public float _speed;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * _speed);
    }
}
