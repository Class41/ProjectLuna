using UnityEngine;

public class light_followplayer : MonoBehaviour
{

    public Transform _player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(_player.position.x, Mathf.Lerp(gameObject.transform.position.y, (_player.position.y + 4), .1f), _player.position.z - 2);
    }
}
