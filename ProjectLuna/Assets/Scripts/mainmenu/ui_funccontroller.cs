﻿/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls button functionality on the main menu scene
 *  
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_funccontroller : MonoBehaviour
{
    public Animator _menuAnim;

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

    public void TutToggle()
    {
        _menuAnim.SetBool("TutorialMode", !_menuAnim.GetBool("TutorialMode"));
    }

}
