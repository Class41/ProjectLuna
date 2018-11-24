using UnityEngine;

public class Terrain_Shop : MonoBehaviour
{
    public GameObject _shopUI, _shopPrompt;
    public Animator _shopAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" && GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            _shopPrompt.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "player" && /*GameObject.FindGameObjectsWithTag("enemy").Length == 0 &&*/ Input.GetKey(KeyCode.E))
        {
            _shopUI.SetActive(true);
            _shopPrompt.SetActive(false);
            _shopAnim.SetBool("shopActive", true);
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// <apra>Closes the shop on screen</apra>
    /// </summary>
    public void CloseShop()
    {
        _shopAnim.SetBool("shopActive", false);
        _shopPrompt.SetActive(true);
        Invoke("DisableShop", 1);
        Time.timeScale = 1;
    }

    /// <summary>
    /// <para>Disables the on-screen shop</para>
    /// </summary>
    public void disableShop()
    {
        _shopUI.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        _shopUI.SetActive(false);
        _shopPrompt.SetActive(false);
    }
}
