using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float _forcemod;
    public Animator _anim;

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("movekeydown", false);
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * _forcemod * Time.deltaTime * 100, ForceMode.Force);
            _anim.SetBool("movekeydown", true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * _forcemod * Time.deltaTime * 100, ForceMode.Force);
            _anim.SetBool("movekeydown", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * _forcemod * Time.deltaTime * 100, ForceMode.Force);
            _anim.SetBool("movekeydown", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * _forcemod * Time.deltaTime * 100, ForceMode.Force);
            _anim.SetBool("movekeydown", true);
        }
    }
}
