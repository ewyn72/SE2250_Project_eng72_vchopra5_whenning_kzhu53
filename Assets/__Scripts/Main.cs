﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//list of weapons and powerups
public enum WeaponType
{
    none,
    blaster,
    spread,
    nuke,
    shield,
    invincibility
}

public class Main : MonoBehaviour
{
    static public Main MAIN_SINGLETON;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
    private float _time = 0.0f;
    private bool _bossSpawned = false;
    private float _delayEnemySpawn = 3f;
    private bool _levelAlreadyShown = false;
    
    public float spawnEverySecond = 2.0f;
    public GameObject[] prefabEnemies;
    public GameObject[] prefabHeroes;
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[]
    {
        WeaponType.shield,
        WeaponType.nuke,
        WeaponType.invincibility
    };

    //sets up each level
    void Awake()
    {
        if (MAIN_SINGLETON == null)
        {
            MAIN_SINGLETON = this;
        }
        else
        {
            print("Main singleton already created.");
        }

        // otherwise char singleton exists
        if (CharacterSelect.CHAR_SINGLETON.luke)
        {
            Instantiate<GameObject>(prefabHeroes[0]);
        }
        else
        {
            Instantiate<GameObject>(prefabHeroes[1]);
        }

        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }

        AudioManager.AUDIO_MANAGER.SwitchScene();
    }

    //Main logic for the game
    void Update()
    {
        if (!Pause.gamePaused)
        {
            _time += Time.deltaTime;
            if (_time >= spawnEverySecond && _time > _delayEnemySpawn)
            {
                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    Destroy(GameObject.Find("Instructions"));
                }
                _delayEnemySpawn = 0;
                _time = _time - spawnEverySecond;
                GameObject enemy;
                int enemyChoice = (int)Random.Range(1, 4);
                if (enemyChoice == 1)
                {
                    enemy = Instantiate<GameObject>(prefabEnemies[0]);
                }
                else if (enemyChoice == 2)
                {
                    enemy = Instantiate<GameObject>(prefabEnemies[1]);
                }
                else
                {
                    enemy = Instantiate<GameObject>(prefabEnemies[2]);
                }
                float xPos = Random.Range(-30, 30);
                enemy.transform.position = new Vector3(xPos, 45f);
            }
            if(_delayEnemySpawn.Equals(0f) && !_levelAlreadyShown)
            {
                Levels.ShowLevel();
                _levelAlreadyShown = true;
            }
            else if (!_delayEnemySpawn.Equals(0f))
            {
                _levelAlreadyShown = false;
            }
        }

        // Check if progress bar is finished
        if (ProgressBar.PROGRESS.finish && _time > _delayEnemySpawn)
        {
            // if first scene move to next level
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _delayEnemySpawn = 3f;
                Levels.Increment();
                Enemy.UpdateEnemy();
                Invoke("NextLevel", 2f);
            }
            // if second scene spawn boss at end of progress bar
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                if (!_bossSpawned)
                {
                    SpawnBoss();
                    _bossSpawned = true;
                }
            }
        }
    }

    //Delay the restart of the game
    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    // Moves game to first game scene
    public void Restart()
    {
        ScoreManager.SCORE_MANAGER.updateCurrScore(0);
        SceneManager.LoadScene("_Scene_0");
    }

    // Moves game to next scene
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    //Get the weapon definition
    static public WeaponDefinition GetWeaponDefinition (WeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }

        return (new WeaponDefinition());
    }

    //the logic for the nuke powerup. Deletes all enemies currently in the scene
    public void nuke()
    {
        GameObject[] enemyList;
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemyList)
        {
            Destroy(enemy);
        }
    }

    //attempts to spawn in a powerup when an enemy is destroyed
    public void ShipDestroyed(Enemy e)
    {
        if(Random.value <= e.powerUpDropChance)
        {
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];

            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            Powerup pu = go.GetComponent<Powerup>();
            pu.SetType(puType);

            pu.transform.position = e.transform.position;
        }
        if (e.eName.Equals("boss")){
            print("Boss destroyed");
            _delayEnemySpawn = 3f;
            Levels.Increment();
            ProgressBar.PROGRESS.CurrentTime = 0;
            ProgressBar.PROGRESS.maxTime += 10;
            Enemy.UpdateEnemy();
            _bossSpawned = false;
        }
    }

    //spawns in the boss for the level
    public void SpawnBoss()
    {
        GameObject enemy = Instantiate<GameObject>(prefabEnemies[3]);
        float xPos = Random.Range(0, 30);
        enemy.transform.position = new Vector3(xPos, 30f, 20);
        if (Levels.currentLevel > 2)
        {
            var enemyBossScript = enemy.GetComponent<EnemyBoss>();
            enemyBossScript.UpdateVarsForLevel(Levels.currentLevel);
        }
    }
}
