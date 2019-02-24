using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0 : Enemy
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
        this.transform.position = position;
    }
}