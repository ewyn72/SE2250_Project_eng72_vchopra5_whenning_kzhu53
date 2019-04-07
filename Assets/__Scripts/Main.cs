using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Update is called once per frame
    void Update()
    {
        if (!Pause.gamePaused)
        {
            _time += Time.deltaTime;
            if (_time >= spawnEverySecond)
            {
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
        }
        /* Work on later
        if (ProgressBar.PROGRESS.finish)
        {
            Invoke("NextLevel", 2f);
        }
        */
    }

    //Delay the restart of the game
    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    }
}
