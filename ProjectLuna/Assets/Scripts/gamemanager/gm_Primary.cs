using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gm_Primary : MonoBehaviour
{
    public GameObject _bossUI;

    public int _gold = 0,
               _score = 1,
               _wave = 0,
               _waveTimeNext = 0;

    public float usedTime = 0,
                 timeAtStartOfwave = 0;

    public float _calculatedWaveTime,
                 _calculatedWaveEndTime,
                 _calculatedEnemySpawns;

    public float _difficultyMultiplier,
                 _enemySpawntimeBetweenEnemySpawnsBase,
                 _enemySpawningEnemiesPerSet,
                 _enemySpawningTimeBetweenSets,
                 _enemyLastKillTime;

    public UnityEngine.UI.Text _goldText,
                               _scoreText,
                               _waveText,
                               _waveTimeText;

    public GameObject _uiWavePanel;
    public Animator _spinnywheel;

    public List<GameObject> enemies_infantry = new List<GameObject>();
    public List<GameObject> enemies_lieutenants = new List<GameObject>();
    public List<GameObject> enemies_generals = new List<GameObject>();

    public float _spawnchanceInfantry = 1.0f,
                 _spawnchanceLieutentant = 0.0f,
                 _spawnchanceGeneral = 0.0f;

    public float _spawnchance_infantry_delta = .05f,
                 _spawnchance_lieutentant_delta = 0.03f,
                 _spawnchance_general_delta = 0.02f;

    public List<Transform> _enemySpawnPositions = new List<Transform>();
    public List<GameObject> waveEntityList = new List<GameObject>();

    //has to be a multiple of 1, ex: .05, .1, .15 etc
    void recalcChances()
    {
        if (_spawnchanceInfantry != 0)
        {
            _spawnchanceLieutentant += _spawnchance_lieutentant_delta;
            _spawnchanceGeneral += _spawnchance_general_delta;
        }

        if (_spawnchanceInfantry >= _spawnchance_infantry_delta)
        {
            _spawnchanceInfantry -= _spawnchance_infantry_delta;
        }
        else
        {
            _spawnchanceInfantry = 0;
        }


    }

    void buildWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float rng = Random.value;

            if (rng <= _spawnchanceInfantry)
            {
                waveEntityList.Add(enemies_infantry[(int)(Random.Range(0, enemies_infantry.Capacity))]);
            }
            else if (rng < _spawnchanceInfantry + _spawnchanceLieutentant && rng >= _spawnchanceInfantry)
            {
                waveEntityList.Add(enemies_lieutenants[(int)(Random.Range(0, enemies_lieutenants.Capacity))]);
            }
            else if (rng < _spawnchanceInfantry + _spawnchanceLieutentant + _spawnchanceGeneral && rng >= _spawnchanceInfantry + _spawnchanceLieutentant)
            {
                waveEntityList.Add(enemies_generals[(int)(Random.Range(0, enemies_generals.Capacity))]);
            }
        }

        recalcChances();
    }

    void spawnWave(int count)
    {
        buildWave(count);
        float setTime = 0.0f;

        for (int i = 1; i <= count; i++)
        {
            Invoke("spawnMob", (i * _enemySpawntimeBetweenEnemySpawnsBase) + setTime);

            if (i % _enemySpawningEnemiesPerSet == 0)
                setTime += _enemySpawningTimeBetweenSets;
        }
    }

    void spawnMob()
    {
        if (waveEntityList.Count > 0)
        {
            var entityentry = waveEntityList[0];

            int randomVal = Random.Range(0, _enemySpawnPositions.Count - 1);

            var enemy = Instantiate(entityentry, _enemySpawnPositions[randomVal].position,
                                                 _enemySpawnPositions[randomVal].rotation);

            if (entityentry.GetComponent<enemy_stats_base>().mobtype == MobType.ENEMY_BOSS)
            {
                var ui = Instantiate(_bossUI,
                                     _enemySpawnPositions[randomVal].position,
                                     _enemySpawnPositions[randomVal].rotation);

                enemy.GetComponent<enemy_navigtaion>().ui = ui;
                ui.GetComponent<UI_enemyline>()._parentObject = enemy;
                ui.GetComponent<UI_follow>()._parentObject = enemy.transform;
                ui.transform.SetParent(GameObject.Find("Entities").transform);
            }

            waveEntityList.RemoveAt(0);
        }
    }

    void calcWave()
    {
        _uiWavePanel.GetComponent<Animator>().SetTrigger("WaveChanged");
        _calculatedEnemySpawns = Mathf.Floor((_difficultyMultiplier * Mathf.Log10(_wave * 5) * _wave) + 5);

        _calculatedWaveTime = _calculatedEnemySpawns * _enemySpawntimeBetweenEnemySpawnsBase + 
                             (_calculatedEnemySpawns % _enemySpawningEnemiesPerSet) *
                             _enemySpawningTimeBetweenSets + 30.0f;

        _calculatedWaveEndTime = Time.timeSinceLevelLoad + _calculatedWaveTime;

        spawnWave((int)_calculatedEnemySpawns);

    }

    public int endingCoin,
               endingPoints;

    public bool interpolingCoins = false,
                interpolingPoints = false;
    public float goldIntertime, 
                 scoreIntertime;

    public void EnemyDeath(int value_coins, int value_points)
    {
        _enemyLastKillTime = Time.timeSinceLevelLoad;
        endingCoin += value_coins;
        endingPoints += value_points;
        _spinnywheel.SetBool("coinsgained", true);

        Invoke("setGoldInter", goldIntertime);
        Invoke("setScoreInter", scoreIntertime);

    }

    public void setGoldInter()
    {
        interpolingCoins = true;
    }

    public void setScoreInter()
    {
        interpolingPoints = true;
    }

    void Start()
    {
        //TESTING DATA. REMOVE LATER
        //TESTING DATA. REMOVE LATER
        //TESTING DATA. REMOVE LATER
        //TESTING DATA. REMOVE LATER
        //TESTING DATA. REMOVE LATER

        _spawnchanceGeneral = .33f;
        _spawnchanceLieutentant = .33f;
        _spawnchanceInfantry = .33f;

        calcWave();
        timeAtStartOfwave = (Mathf.Abs(Time.timeSinceLevelLoad - _calculatedWaveTime - usedTime));

    }

    void LateUpdate()
    {
        AnimatorClipInfo[] m_CurrentClipInfo;
        m_CurrentClipInfo = _spinnywheel.GetCurrentAnimatorClipInfo(0);

        if (interpolingCoins && m_CurrentClipInfo[0].clip.name == "idlewithgold")
        {
            _gold++;

            if (_gold >= endingCoin)
            {
                interpolingCoins = false;
            }
        }

        if (interpolingPoints && m_CurrentClipInfo[0].clip.name == "idlewithscore")
        {
            _score++;

            if (_score >= endingPoints)
            {
                interpolingPoints = false;
                _spinnywheel.SetBool("coinsgained", false);
            }
        }

        _waveTimeNext = (int)(Mathf.Abs(Time.timeSinceLevelLoad - _calculatedWaveTime - usedTime));
        _goldText.text = _gold.ToString();
        _scoreText.text = _score.ToString();
        _waveText.text = _wave.ToString();
        _waveTimeText.text = _waveTimeNext.ToString();

        _spinnywheel.SetFloat("timesincelastkill", Time.timeSinceLevelLoad - _enemyLastKillTime);

        if (_calculatedWaveEndTime < Time.timeSinceLevelLoad)
        {
           _wave++;
            usedTime += _calculatedWaveTime;
           calcWave();
           timeAtStartOfwave = (Mathf.Abs(Time.timeSinceLevelLoad - _calculatedWaveTime - usedTime));
        }
    }
}
