using UnityEngine;

public class player_clamps : MonoBehaviour
{


    public float max_x,
                 min_x,
                 max_y,
                 min_y,
                 max_z,
                 min_z;

    void Update()
    {
        Vector3 clampedSmoothedPos = new Vector3(
                                                Mathf.Clamp(transform.position.x, min_x, max_x),
                                                Mathf.Clamp(transform.position.y, min_y, max_y),
                                                Mathf.Clamp(transform.position.z, min_z, max_z)
                                                );
        transform.position = clampedSmoothedPos;
    }
}
