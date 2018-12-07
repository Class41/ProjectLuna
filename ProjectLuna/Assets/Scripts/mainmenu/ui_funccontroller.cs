/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls button functionality on the main menu scene
 *  
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections.Generic;

public class ui_funccontroller : MonoBehaviour
{
    public Animator _menuAnim;
    public Animator _playerAnim;
    public Text _tutText;
    public GameObject _loadAnim;
    public NavMeshAgent _playerAgent;
    public static bool _finished;
    public bool _lockout;


    private Animator _currentTutorialButtonAnim;
    private int _currentIndex = -1;
    public List<GameObject> _tutPages = new List<GameObject>();

    public void SetTutorialButtonPressed(Animator anim)
    {
        if (_currentTutorialButtonAnim != null)
            _currentTutorialButtonAnim.SetBool("selected", false);

        anim.SetBool("selected", true);
        _currentTutorialButtonAnim = anim;
    }

    public void SetTutorialIndexOpen(int index)
    {
        if(_currentIndex > 0)
        {
            _tutPages[_currentIndex].SetActive(false);
        }
            
        _tutPages[index].SetActive(true);
        _currentIndex = index;
    }

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void LoadGame()
    {
        if(!_lockout)
            StartCoroutine(AsyncLoad());
        _lockout = true;
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("main", LoadSceneMode.Single);
        asyncScene.allowSceneActivation = false;
        _loadAnim.SetActive(true);
        while (asyncScene.progress < .9f)
            yield return null;


        _menuAnim.SetBool("Loaded", true);
        _playerAgent.SetDestination(new Vector3(0.0f, -.75f, -6.93f));
        _playerAnim.SetBool("movekeydown", true);

        while (!_finished)
            yield return null;


        asyncScene.allowSceneActivation = true;
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
        if (_currentTutorialButtonAnim != null)
        {
            _currentTutorialButtonAnim.SetBool("selected", false);

            StartCoroutine(TutMenuAsync());
        }
        else
        {
            if (!_lockout)
            {
                _menuAnim.SetBool("TutorialMode", !_menuAnim.GetBool("TutorialMode"));
                Invoke("SwapTutText", .55f);
            }
        }
    }

    IEnumerator TutMenuAsync()
    {
        while (!_currentTutorialButtonAnim.GetCurrentAnimatorStateInfo(0).IsName("btnidle"))
            yield return null;

        _tutPages[_currentIndex].SetActive(false);
        _currentIndex = -1;

        if (!_lockout)
        {
            _menuAnim.SetBool("TutorialMode", !_menuAnim.GetBool("TutorialMode"));
            Invoke("SwapTutText", .55f);
            _currentTutorialButtonAnim = null;
        }
    }

}
