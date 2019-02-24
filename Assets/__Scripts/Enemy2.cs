using UnityEngine;
using System.Collections;

public class Enemy2 : Enemy
{
    private float speedX;
    private float speedY;

    private bool turnOtherWay = false;

    public void Start()
    {
        speedX = speed;
        speedY = speed;
    }

    public void Update()
    {
        Move();
    }

    override
    public void Move()
    {
        Vector3 position = this.transform.position;
        position.y -= speedY * Time.deltaTime;
        position.x -= speedX * Time.deltaTime;
        this.transform.position = position;

        if (!turnOtherWay && speedX < speed + 20)
        {
            speedX += 1;
            speedY -= 1;
        }
        else if (speedX > speed-30 && speedY < speed+30)
        {
            speedX -= 1;
            speedY += 1;
        }

        if (speedY <= 0)
        {
            turnOtherWay = true;
        }
    }

}
