﻿/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: shop controller for world objects that toggle the shop
 *  
 */

using UnityEngine;

public class Terrain_Shop : MonoBehaviour
{
    public GameObject _shopUI, _shopPrompt;
    public Animator _shopAnim;
    public ui_shoplogic _shopLogic;
    public Animator _deadAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" && GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            _shopPrompt.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "player" && GameObject.FindGameObjectsWithTag("enemy").Length == 0 && Input.GetKey(KeyCode.E))
        {
            ShowShop();
        }
    }

    public void ShowShop()
    {
        _shopLogic.DoShopChecks();
        _shopUI.SetActive(true);
        _shopPrompt.SetActive(false);
        _shopAnim.SetBool("shopActive", true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// <apra>Closes the shop on screen</apra>
    /// </summary>
    public void CloseShop()
    {
        _shopAnim.SetBool("shopActive", false);
        if(!_deadAnim.GetBool("Dead"))
            _shopPrompt.SetActive(true);
        Invoke("DisableShop", 1);
        Time.timeScale = 1;
    }

    /// <summary>
    /// <para>Disables the on-screen shop</para>
    /// </summary>
    public void DisableShop()
    {
        _shopUI.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        _shopUI.SetActive(false);
        _shopPrompt.SetActive(false);
    }
}
