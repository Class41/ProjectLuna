using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float _forcemod;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * _forcemod * Time.deltaTime * 100, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * _forcemod * Time.deltaTime * 100, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * _forcemod * Time.deltaTime * 100, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * _forcemod * Time.deltaTime * 100, ForceMode.Force);
        }

        anim.SetFloat("Velocity", gameObject.GetComponent<Rigidbody>().velocity.magnitude);
    }
}
