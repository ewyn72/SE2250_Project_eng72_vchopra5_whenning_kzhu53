using UnityEngine;
using System.Collections;

public class Enemy2 : Enemy
{
    private float waveFrequency = 2;
    private float waveWidth = 8;
    private float waveRotY = 45;

    private float x0;
    private float birthtime;

    public void Start()
    {
        speed = 20;
        x0 = transform.position.x;
        birthtime = Time.time;
    }


    public override void Move()
    {
        Vector3 temppos = transform.position;

        float age = Time.time - birthtime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        temppos.x = x0 + waveWidth * sin;
        transform.position = temppos;

        Vector3 rotate = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rotate);

        base.Move();
    }

}
