using UnityEngine;
using System.Collections;

public class Enemy1 : Enemy
{

    public void Start()
    {

    }

    public void Update()
    {
        Move();
    }

    override
    public void Move()
    {
        Vector3 position = this.transform.position;
        position.y -= speed * Time.deltaTime;
        position.x -= speed * Time.deltaTime;
        this.transform.position = position;
    }

}
