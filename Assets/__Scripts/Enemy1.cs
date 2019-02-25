using UnityEngine;
using System.Collections;

public class Enemy1 : Enemy
{

    public void Update()
    {
        Move();
    }


    public override void Move()
    {
        Vector3 position = this.transform.position;
        position.x -= speed * Time.deltaTime;
        position.y -= speed * Time.deltaTime;
        this.transform.position = position;
    }

}
