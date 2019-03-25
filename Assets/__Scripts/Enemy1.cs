using UnityEngine;
using System.Collections;

public class Enemy1 : Enemy
{
    private int _leftOrRight = 0;

    public void Start()
    {
        health = 2f;
        score = 50f;
       _leftOrRight = (int)Random.Range(0, 2);
    }


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
