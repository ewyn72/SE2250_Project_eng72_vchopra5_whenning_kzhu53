using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    // <summary>
    // Boss will choose a random point to go to
    // Then they will go to a new random point
    // and repeat until the player shoots them down
    // <summary>

    public GameObject projPrefab;       //The projectile prefab for the boss's projectile
    public float projSpeed;             //The projectile speed of the boss
    private float _time;                //The time used for boss firing
    private Vector3 _p0, _p1;           //Two points to interpolate
    private float _timeStart;           //the time used for movement
    private float _duration = 5;        //How long the boss stays there
    float projectileSpeedScaler = 1;    //A scaling used to increase the speed of the boss projectile

    private float _fireRate = 2f;
    private bool _prevPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        //Health, score, projectile spee and fire rate dependent on level
        health = health + 20*Mathf.Max(Levels.LEVEL_SINGLETON.currentLevel - 2, 0);
        score = score + 500 * Mathf.Max(Levels.LEVEL_SINGLETON.currentLevel - 2, 0);

        _fireRate = Mathf.Max(Levels.LEVEL_SINGLETON.currentLevel - 2, _fireRate);
        projSpeed = Mathf.Max(Levels.LEVEL_SINGLETON.currentLevel - 2, projSpeed);
        //Sets the initial time
        _time = Time.time;

        //Sets the initial positions
        _p0 = _p1 = transform.position;

        eName = "boss";
        InitMovement();
    }

    void InitMovement()
    {
        _p0 = _p1;  //Sets p0 to old p1
                    //Assigning new position

        //The addition/subtractions account for model size
        _p1.x = Random.Range(0, 30);
        _p1.y = Random.Range(30, 39);

        //Resets time
        _timeStart = Time.time;
    }

    public override void Move()
    {

        //Only fire if it is not paused and only move if it is not paused
        if (!Pause.IS_PAUSED)
        {
            Fire();

            //This overrides the Enemy.Move() with a linear interpolation
            float u = (Time.time - _timeStart) / _duration;

            if (u >= 1 || _prevPaused)
            {
                if (_prevPaused)
                {
                    _p1 = transform.position;
                }
                InitMovement();
                u = 0;
                _prevPaused = false;
            }

            u = 1 - Mathf.Pow(1 - u, 2);                    //Apply Ease Out easing to u (Moves fast at beginning and slows as it approaches _p1)
            transform.position = (1 - u) * _p0 + u * _p1;   //Simple linear interpolation
        }
        //Set the time to be the current time if it is paused to ensure that the boss doesn't immediately fire after unpausing
        else
        {

            _time = Time.time;
            _prevPaused = true;
        }

    }
    
    public void Fire()
    {
        //Only fire if not paused
        if (!Pause.IS_PAUSED)
        {
            //The boss fires every few seconds
            if (Time.time >= _time + _fireRate)
            {
                //Creates 3 projectiles and sets there location relative to the boss's location so they come from different points
                GameObject projectile1 = Instantiate<GameObject>(projPrefab);
                GameObject projectile2 = Instantiate<GameObject>(projPrefab);
                GameObject projectile3 = Instantiate<GameObject>(projPrefab);
                projectile1.transform.position = transform.position + new Vector3(-14.25f, -46, -20);
                Rigidbody rigidB1 = projectile1.GetComponent<Rigidbody>();
                rigidB1.velocity = Vector3.down * projSpeed * projectileSpeedScaler;

                projectile2.transform.position = transform.position + new Vector3(-19.25f, -28, -20);
                Rigidbody rigidB2 = projectile2.GetComponent<Rigidbody>();
                rigidB2.velocity = Vector3.down * projSpeed * projectileSpeedScaler;

                projectile3.transform.position = transform.position + new Vector3(-8.25f, -28, -20);
                Rigidbody rigidB3 = projectile3.GetComponent<Rigidbody>();
                rigidB3.velocity = Vector3.down * projSpeed * projectileSpeedScaler;

                //Reseting the time variable and increasing the speed of the boss projectile
                _time = Time.time;
                projectileSpeedScaler = projectileSpeedScaler + 0.05f;
            }
        }
        //Set the time to be the current time if it is paused to ensure that the boss doesn't immediately fire after unpausing
        else
        {
            _time = Time.time;
        }
    }


}
