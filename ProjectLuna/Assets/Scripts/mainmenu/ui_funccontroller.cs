using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_funccontroller : MonoBehaviour
{
    public Animator _menuAnim;

    public void LoadGame()
    {
        SceneManager.LoadScene("main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TutToggle()
    {
        _menuAnim.SetBool("TutorialMode", !_menuAnim.GetBool("TutorialMode"));
    }

}
