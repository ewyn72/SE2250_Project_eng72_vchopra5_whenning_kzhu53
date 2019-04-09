using UnityEngine;
using System.Collections;

public class Enemy1 : Enemy
{
    private int _leftOrRight = 0;

    //Set health and score, and whether it moves left or right
    public void Start()
    {
        //Health and score dependent on level
        health = 2f + Mathf.Max(Levels.LEVEL_SINGLETON.currentLevel-2, 0);
        score = 50f + 30*Mathf.Max(Levels.LEVEL_SINGLETON.currentLevel - 2, 0); 
        powerUpDropChance = 0.5f;
        _leftOrRight = (int)Random.Range(0, 2);
    }


    //Move diagonally
    public override void Move()
    {
        Vector3 position = this.transform.position;

        if (_leftOrRight == 0)
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (_leftOrRight == 1)
        {
            position.x += speed * Time.deltaTime;
        }

        this.transform.position = position;

        base.Move();
    }

}
