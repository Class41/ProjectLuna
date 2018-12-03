/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls core game mechanics such as UI, enemy spawns, difficulty and time.
 *  
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gm_Primary : MonoBehaviour
{
    [Header("UI References")]
    public GameObject _bossUI,
                      _debugConsole,
                      _fpsAndPCStats;

    [Header("Essential Game Values")]
    public int _gold = 0,
               _score = 0,
               _wave = 0,
               _waveTimeNext = 0;

    [Header("Wave Times")]
    public float _usedTime = 0,
                 _timeAtStartOfwave = 0;

    public float _calculatedWaveTime,
                 _calculatedWaveEndTime,
                 _calculatedEnemySpawns;

    [Header("Difficulty Values")]
    public float _difficultyMultiplier,
                 _enemySpawntimeBetweenEnemySpawnsBase,
                 _enemySpawningEnemiesPerSet,
                 _enemySpawningTimeBetweenSets,
                 _enemyLastKillTime;

    [Header("UI References")]
    public UnityEngine.UI.Text _goldText,
                               _scoreText,
                               _waveText,
                               _waveTimeText;


    public GameObject _uiWavePanel, _uiShop;

    [Header("UI Element References")]
    public Animator _spinnywheel,
                    _pauseMenu,
                    _deathUI;

    [Header("Enemy Spawn Controlpanel")]
    public List<GameObject> enemies_infantry = new List<GameObject>();
    public List<GameObject> enemies_lieutenants = new List<GameObject>();
    public List<GameObject> enemies_generals = new List<GameObject>();

    public float _spawnchanceInfantry = 1.0f,
                 _spawnchanceLieutentant = 0.0f,
                 _spawnchanceGeneral = 0.0f;

    public float _spawnchance_infantry_delta = .05f,
                 _spawnchance_lieutentant_delta = 0.03f,
                 _spawnchance_general_delta = 0.02f;

    float _waveSpawnMultiplierMax;

    [Header("Enemy Spawn Positions and Wave Details")]
    public List<Transform> enemySpawnPositions = new List<Transform>();
    public List<GameObject> waveEntityList = new List<GameObject>();

    [Header("Player reference")]
    public player_health _playerHp;

    //has to be a multiple of 1, ex: .05, .1, .15 etc
    /// <summary>
    /// <para>Calculates enemy spawn chances based on their tier</para>
    /// </summary>
    private void RecalcEnemySpawnChances()
    {
        if(_wave <= _waveSpawnMultiplierMax)
        {
            _spawnchanceInfantry = 1.0f - (_spawnchance_infantry_delta * _wave);
            _spawnchanceLieutentant = _spawnchance_lieutentant_delta * _wave;
            _spawnchanceGeneral = _spawnchance_general_delta * _wave;
        }
        else
        {
            _spawnchanceInfantry -= _spawnchance_infantry_delta * _waveSpawnMultiplierMax;
            _spawnchanceLieutentant += _spawnchance_lieutentant_delta * _waveSpawnMultiplierMax;
            _spawnchanceGeneral += _spawnchance_general_delta * _waveSpawnMultiplierMax;
        }
    }

    /// <summary>
    /// builds a parameterized number of enemies based on current spawn chances for enemies
    /// </summary>
    /// <param name="count"></param>
    private void BuildEnemyWaveWave(int count)
    {
        RecalcEnemySpawnChances();
        for (int i = 0; i < count; i++)
        {
            float rng = Random.value;

            if (rng <= _spawnchanceInfantry)
            {
                waveEntityList.Add(enemies_infantry[Random.Range(0, enemies_infantry.Capacity)]);
            }
            else if (rng < _spawnchanceInfantry + _spawnchanceLieutentant && rng >= _spawnchanceInfantry)
            {
                waveEntityList.Add(enemies_lieutenants[Random.Range(0, enemies_lieutenants.Capacity)]);
            }
            else if (rng < _spawnchanceInfantry + _spawnchanceLieutentant + _spawnchanceGeneral && rng >= _spawnchanceInfantry + _spawnchanceLieutentant)
            {
                waveEntityList.Add(enemies_generals[Random.Range(0, enemies_generals.Capacity)]);
            }
        }
    }

    /// <summary>
    /// <para>Spawns the built enemy wave</para>
    /// </summary>
    /// <param name="count"></param>
    private void SpawnEnemyWave(int count)
    {
        BuildEnemyWaveWave(count);
        float setTime = 0.0f;

        for (int i = 1; i <= count; i++)
        {
            Invoke("SpawnMob", (i * _enemySpawntimeBetweenEnemySpawnsBase) + setTime);

            if (i % _enemySpawningEnemiesPerSet == 0)
                setTime += _enemySpawningTimeBetweenSets;
        }
    }

    /// <summary>
    /// <para>Spawns an entity from the wave list</para>
    /// </summary>
    void SpawnMob()
    {
        if (waveEntityList.Count > 0)
        {
            var entityentry = waveEntityList[0];

            int randomVal = Random.Range(0, enemySpawnPositions.Count - 1);

            var enemy = Instantiate(entityentry, enemySpawnPositions[randomVal].position,
                                                 enemySpawnPositions[randomVal].rotation);

            if (entityentry.GetComponent<enemy_stats_base>()._mobtype == MobType.ENEMY_BOSS)
            {
                var ui = Instantiate(_bossUI,
                                     enemySpawnPositions[randomVal].position,
                                     enemySpawnPositions[randomVal].rotation);

                enemy.GetComponent<enemy_navigtaion>()._ui = ui;
                ui.GetComponent<UI_enemyline>()._parentObject = enemy;
                ui.GetComponent<UI_follow>()._parentObject = enemy.transform;
                ui.transform.SetParent(GameObject.Find("Entities").transform);
            }

            waveEntityList.RemoveAt(0);
        }
    }

    /// <summary>
    /// <para>Calculates how many enemies should spawn per wave and calculates wave time</para>
    /// </summary>
    private void StartWaveCalculations()
    {
        _uiWavePanel.GetComponent<Animator>().SetTrigger("WaveChanged");
        _calculatedEnemySpawns = Mathf.Floor((_difficultyMultiplier * Mathf.Log10(_wave * 5) * _wave) + 5);

        _calculatedWaveTime = _calculatedEnemySpawns * _enemySpawntimeBetweenEnemySpawnsBase*2 +
                             (_calculatedEnemySpawns % _enemySpawningEnemiesPerSet) *
                             (_enemySpawningTimeBetweenSets*2) + 30.0f;

        _calculatedWaveEndTime = Time.timeSinceLevelLoad + _calculatedWaveTime;

        SpawnEnemyWave((int)_calculatedEnemySpawns);
    }

    [Header("Coins and Score")]
    public int _endingCoin,
               _endingPoints;

    public bool _interpolingCoins = false,
                _interpolingPoints = false;
    public float _goldIntertime,
                 _scoreIntertime;

    /// <summary>
    /// <para>Called by dying entities to give player gold and score</para>
    /// </summary>
    /// <param name="value_coins"></param>
    /// <param name="value_points"></param>
    public void EnemyDeath(int value_coins, int value_points, int value_health)
    {
        _enemyLastKillTime = Time.timeSinceLevelLoad;
        _endingCoin += value_coins;
        _endingPoints += value_points;
        _playerHp.HealthHeal(value_health);

        PlayerPrefs.SetInt("gold", _endingCoin);
        PlayerPrefs.SetInt("score", _endingPoints);

        _spinnywheel.SetBool("coinsgained", true);
        Invoke("SetGoldInter", _goldIntertime);
        Invoke("SetScoreInter", _scoreIntertime);
    }

    /// <summary>
    /// <para>Sets if we are interpolating gold on the UI</para>
    /// </summary>
    public void SetGoldInter()
    {
        _interpolingCoins = true;
    }

    /// <summary>
    /// <para>Sets if we are interpolating score on the UI</para>
    /// </summary>
    public void SetScoreInter()
    {
        _interpolingPoints = true;
    }

    /// <summary>
    /// <para>Called by the UI to exit the game</para>
    /// </summary>
    public void ExitGame()
    {
        if (_pauseMenu.GetBool("menuopened") || _deathUI.GetBool("Dead"))
            Application.Quit();
    }

    /// <summary>
    /// <para>Called by the UI to exit to menu</para>
    /// </summary>
    public void LoadMenu()
    {
        if(_pauseMenu.GetBool("menuopened") || _deathUI.GetBool("Dead"))
            SceneManager.LoadScene("menu");
    }

    void Start()
    {
        _waveSpawnMultiplierMax = 1.0f / _spawnchance_infantry_delta;
        PullConfigValues();

        StartWaveCalculations();
        _timeAtStartOfwave = (Mathf.Abs(Time.timeSinceLevelLoad - _calculatedWaveTime - _usedTime));

    }

    /// <summary>
    /// <para>Used to reset the gamestate to initial state</para>
    /// </summary>
    public void ResetProgress()
    {
        if (_pauseMenu.GetBool("menuopened"))
        {
            PlayerPrefs.SetInt("gold", 1);
            PlayerPrefs.SetInt("score", 1);
            PlayerPrefs.SetInt("healthlevel", 1);
            PlayerPrefs.SetInt("armorlevel", 1);
            PlayerPrefs.SetInt("wave", 1);
            LoadMenu();
        }
    }

    /// <summary>
    /// <para>Updates gold and score from config</para>
    /// </summary>
    public void PullConfigValues()
    {
        if (!PlayerPrefs.HasKey("gold"))
            PlayerPrefs.SetInt("gold", 1);

        if (!PlayerPrefs.HasKey("score"))
            PlayerPrefs.SetInt("score", 1);

        if (!PlayerPrefs.HasKey("healthlevel"))
            PlayerPrefs.SetInt("healthlevel", 1);

        if (!PlayerPrefs.HasKey("armorlevel"))
            PlayerPrefs.SetInt("armorlevel", 1);

        if (!PlayerPrefs.HasKey("wave"))
            PlayerPrefs.SetInt("wave", 1);

        _wave = PlayerPrefs.GetInt("wave");
        _gold = PlayerPrefs.GetInt("gold");
        _score = PlayerPrefs.GetInt("score");
        _endingCoin = _gold + 1;
        _endingPoints = _score + 1;
    }

    void LateUpdate()
    {
        AnimatorClipInfo[] m_CurrentClipInfo;
        m_CurrentClipInfo = _spinnywheel.GetCurrentAnimatorClipInfo(0);

        if (_interpolingCoins && m_CurrentClipInfo[0].clip.name == "idlewithgold")
        {
            _gold++;

            if (_gold >= _endingCoin)
            {
                _interpolingCoins = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Return))
        {
            _debugConsole.SetActive(!_debugConsole.activeSelf);
            _fpsAndPCStats.SetActive(!_fpsAndPCStats.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !_uiShop.activeSelf)
        {
            _pauseMenu.SetBool("menuopened", !_pauseMenu.GetBool("menuopened"));

            if (_pauseMenu.GetBool("menuopened"))
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        if (_interpolingPoints && m_CurrentClipInfo[0].clip.name == "idlewithscore")
        {
            _score++;
            _spinnywheel.SetInteger("scorecountremaining", _endingPoints - _score);

            if (_score >= _endingPoints)
            {
                _interpolingPoints = false;
                _spinnywheel.SetBool("coinsgained", false);
            }
        }

        _waveTimeNext = (int)(Mathf.Abs(Time.timeSinceLevelLoad - _calculatedWaveTime - _usedTime));
        _goldText.text = _gold.ToString();
        _scoreText.text = _score.ToString();
        _waveText.text = _wave.ToString();
        _waveTimeText.text = _waveTimeNext.ToString();

        _spinnywheel.SetFloat("timesincelastkill", Time.timeSinceLevelLoad - _enemyLastKillTime);

        if (_calculatedWaveEndTime < Time.timeSinceLevelLoad)
        {
            _wave++;
            PlayerPrefs.SetInt("wave", _wave);
            _usedTime += _calculatedWaveTime;
            StartWaveCalculations();
            _timeAtStartOfwave = (Mathf.Abs(Time.timeSinceLevelLoad - _calculatedWaveTime - _usedTime));
        }
    }
}
