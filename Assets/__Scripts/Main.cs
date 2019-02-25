using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private float _time = 0.0f;

    public float spawnEverySecond = 2.0f;
    public GameObject[] prefabEnemies;

    // Update is called once per frame
    void Update()
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
}
