using UnityEngine;
using System.Collections;

public class Enemy2 : Enemy
{
    public float waveFrequency = 2;
    public float waveWidth = 8;
    public float waveRotY = 20;

    private float _x0;
    private float _birthtime;

    //Set health, score and how long it has been alive for
    public void Start()
    {
        health = 3f;
        score = 150f;
        speed = 20;
        powerUpDropChance = 0.8f;
        _x0 = transform.position.x;
        _birthtime = Time.time;
    }


    //Move in a sinusoidal pattern
    public override void Move()
    {
        Vector3 tempPos = transform.position;

        float age = Time.time - _birthtime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = _x0 + waveWidth * sin;
        transform.position = tempPos;

        Vector3 rotate = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rotate);

        base.Move();
    }

}
