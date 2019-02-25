using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private float time = 0.0f;
    public GameObject enemy0;
    public GameObject enemy1;
    public GameObject enemy2;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 3.0f)
        {
            time = time - 3.0f;
            GameObject enemy;
            float enemyChoice = Random.Range(1, 4);
            if (enemyChoice == 1)
            {
                enemy = Instantiate<GameObject>(enemy0);
            }
            else if (enemyChoice == 2)
            {
                enemy = Instantiate<GameObject>(enemy1);
            }
            else
            {
                enemy = Instantiate<GameObject>(enemy2);
            }
            float xPos = Random.Range(-50, 50);
            enemy.transform.position = new Vector3(xPos, 50f);
        }
    }
}
