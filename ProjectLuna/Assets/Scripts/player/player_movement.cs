using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float _forcemod;
    public Animator _anim;

    public Material _barrierMat;
    public float _barrierVisibility = 0,
                 _barrierDisappearTime = 0;

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.CompareTag("invisbarrierwall"))
        {
            _barrierVisibility = .25f;
        }
    }

    void Update()
    {

        if(_barrierVisibility > 0)
        {
            _barrierMat.SetFloat("Vector1_B528FB43", _barrierVisibility);
            _barrierVisibility = Mathf.Lerp(_barrierVisibility, 0.0f, _barrierDisappearTime);
        }

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
