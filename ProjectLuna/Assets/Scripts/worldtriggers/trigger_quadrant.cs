using UnityEngine;

public class trigger_quadrant : MonoBehaviour
{
    public bool playerContained;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player"))
        {
            playerContained = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            playerContained = false;

        }
    }
}
