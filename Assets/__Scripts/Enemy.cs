using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int score = 100;

    public float radius = 1f;

    public virtual void Move()
    {
        Vector3 position = this.transform.position;
        position.y -= speed * Time.deltaTime;
        this.transform.position = position;
    }

    public void Update()
    {
        Move();
    }
}
