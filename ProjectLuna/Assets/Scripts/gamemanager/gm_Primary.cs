using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gm_Primary : MonoBehaviour
{

    public int _gold = 0,
               _score = 1,
               _wave = 0,
               _waveTimeNext = 0;

    public int _waveTimeBase = 1,
               _enemySpawnsBase = 1;


    public List<Transform> _enemySpawnPositions = new List<Transform>();


    public float _difficultyMultiplier = 1.0f,
                 _enemySpawntimeBase = 1.0f;

    public UnityEngine.UI.Text _goldText,
                               _scoreText,
                               _waveText,
                               _waveTimeText;

    public GameObject _bossUI;

    public List<GameObject> enemies_infantry = new List<GameObject>();
    public List<GameObject> enemies_lieutenants = new List<GameObject>();
    public List<GameObject> enemies_generals = new List<GameObject>();

    public float _spawnchanceInfantry = 1.0f,
                 _spawnchanceLieutentant = 0.0f,
                 _spawnchanceGeneral = 0.0f;

    public float _spawnchance_infantry_delta = .05f,
                 _spawnchance_lieutentant_delta = 0.03f,
                 _spawnchance_general_delta = 0.02f;

    public List<GameObject> wave = new List<GameObject>();

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
                wave.Add(enemies_infantry[(int)(Random.Range(0, enemies_infantry.Capacity))]);
            }
            else if (rng < _spawnchanceInfantry + _spawnchanceLieutentant && rng >= _spawnchanceInfantry)
            {
                wave.Add(enemies_lieutenants[(int)(Random.Range(0, enemies_lieutenants.Capacity))]);
            }
            else if (rng < _spawnchanceInfantry + _spawnchanceLieutentant + _spawnchanceGeneral && rng >= _spawnchanceInfantry + _spawnchanceLieutentant)
            {
                wave.Add(enemies_generals[(int)(Random.Range(0, enemies_generals.Capacity))]);
            }
        }

        recalcChances();
    }

    void spawnWave(int count)
    {
        buildWave(count);
        for (int i = 1; i <= count; i++)
        {
            Invoke("spawnMob", i * _enemySpawntimeBase);
        }
    }

    void spawnMob()
    {
        if (wave.Count > 0)
        {
            var entityentry = wave[0];

            int randomVal = Random.Range(0, _enemySpawnPositions.Count - 1);

            var enemy = Instantiate(entityentry, _enemySpawnPositions[randomVal].position,
                                                 _enemySpawnPositions[randomVal].rotation);

            if (entityentry.GetComponent<enemy_stats_base>().mobtype == MobType.ENEMY_BOSS)
            {
                var ui = Instantiate(_bossUI,
                                     _enemySpawnPositions[randomVal].position,
                                     _enemySpawnPositions[randomVal].rotation);

                enemy.GetComponent<enemy_navigtaion>().ui = ui;
                ui.GetComponent<UI_follow>()._parentObject = enemy.transform;
                ui.transform.SetParent(GameObject.Find("Entities").transform);
            }

            wave.RemoveAt(0);
        }
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
        spawnWave(50);

        //TESTING DATA. REMOVE LATER
        //TESTING DATA. REMOVE LATER
        //TESTING DATA. REMOVE LATER
        //TESTING DATA. REMOVE LATER
        //TESTING DATA. REMOVE LATER
    }


    void Update()
    {
        _goldText.text = _gold.ToString();
        _scoreText.text = _score.ToString();
        _waveText.text = _wave.ToString();
        _waveTimeText.text = _waveTimeNext.ToString();
    }
}
