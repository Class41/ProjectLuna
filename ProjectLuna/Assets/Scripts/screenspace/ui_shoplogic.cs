﻿/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls the game logic of the shop
 *  
 */

using UnityEngine;
using UnityEngine.UI;

public class ui_shoplogic : MonoBehaviour
{
    public GameObject _hpUpgradeButton,
                      _armorUpgradeButton,
                      _restoreSanityButton;

    public Text _shopHPText, 
                _shopArmorText;

    public Text _hpUpgradeButtonText,
                _armorUpgradeButtonText;

    public Text _restoreSanityText;

    public int _hpUpgradeCost,
               _armorUpgradeCost;

    public player_health _playerStats;

    public Animator _deathScreenAnimator;
    
    public gm_Primary _gamemanagerPrimary;

    /// <summary>
    /// <para>Checks what they player can and cannot afford.</para>
    /// </summary>
    public void DoShopChecks()
    {
        _hpUpgradeCost = (int)(60 * Mathf.Pow(Mathf.Log10(Mathf.Pow(PlayerPrefs.GetInt("healthlevel"), 3)), 2));
        _armorUpgradeCost = (int)(60 * Mathf.Pow(Mathf.Log10(Mathf.Pow(PlayerPrefs.GetInt("armorlevel"), 3)), 2));

        if (PlayerPrefs.GetInt("gold") < _hpUpgradeCost || PlayerPrefs.GetInt("healthlevel") >= 100)
            _hpUpgradeButton.SetActive(false);
        else
            _hpUpgradeButton.SetActive(true);

        if (PlayerPrefs.GetInt("gold") < _armorUpgradeCost || PlayerPrefs.GetInt("armorlevel") >= 100)
            _armorUpgradeButton.SetActive(false);
        else
            _armorUpgradeButton.SetActive(true);

        if ((int)(_playerStats._maxHealth - _playerStats._health) > PlayerPrefs.GetInt("gold") - 10 || _deathScreenAnimator.GetBool("Dead"))
            _restoreSanityButton.SetActive(false);
        else
            _restoreSanityButton.SetActive(true);


        _shopHPText.text = string.Format("Sanity - {0}/100", PlayerPrefs.GetInt("healthlevel"));
        _shopArmorText.text = string.Format("Devotion - {0}/100", PlayerPrefs.GetInt("armorlevel"));
        _hpUpgradeButtonText.text = string.Format("Upgrade: {0} coins", _hpUpgradeCost);
        _armorUpgradeButtonText.text = string.Format("Upgrade: {0} coins", _armorUpgradeCost);
        _restoreSanityText.text = string.Format("Restore Sanity\n{0} coins", (int)(_playerStats._maxHealth - _playerStats._health));
    }

    /// <summary>
    /// <para>Upgrades armor/damage</para>
    /// </summary>
    public void UpgradeArmor()
    {
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") -_armorUpgradeCost);
        PlayerPrefs.SetInt("armorlevel", PlayerPrefs.GetInt("armorlevel") + 1);
        _playerStats.CalculateStats();
        DoShopChecks();
        _gamemanagerPrimary.PullConfigValues();
    }

    /// <summary>
    /// <para>Upgrades health/sanity</para>
    /// </summary>
    public void UpgradeHealth()
    {
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - _hpUpgradeCost);
        PlayerPrefs.SetInt("healthlevel", PlayerPrefs.GetInt("healthlevel") + 1);
        _playerStats.CalculateStats();
        DoShopChecks();
        _gamemanagerPrimary.PullConfigValues();
    }

    /// <summary>
    /// <para>Restores player health</para>
    /// </summary>
    public void RestoreHealth()
    {
        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - (int)(_playerStats._maxHealth - _playerStats._health));
        _playerStats._health = _playerStats._maxHealth;
        DoShopChecks();
        _gamemanagerPrimary.PullConfigValues();
    }

}
