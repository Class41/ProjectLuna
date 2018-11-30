/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls button functionality on the main menu scene
 *  
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ui_funccontroller : MonoBehaviour
{
    public Animator _menuAnim;
    public Text _tutText;

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SwapTutText()
    {
        if (_tutText.text == "Tutorial")
            _tutText.text = "Back";
        else
            _tutText.text = "Tutorial";
    }

    /// <summary>
    /// <para>Called by UI to toggle tut mode</para>
    /// </summary>
    public void TutToggle()
    {
        _menuAnim.SetBool("TutorialMode", !_menuAnim.GetBool("TutorialMode"));
        Invoke("SwapTutText", .55f);
    }

}
