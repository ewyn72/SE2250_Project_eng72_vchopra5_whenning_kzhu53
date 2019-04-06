using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero PLAYER_HERO;

    private GameObject _lastTriggerGo = null;

    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public bool start = true;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLvl = 1;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    void Awake()
    {
        if (PLAYER_HERO == null)
        {
            PLAYER_HERO = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get info from input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Cool fly-in at start
        if (start)
        {
            pos.y += speed * Time.deltaTime;
            transform.position = pos;
        }
        if (pos.y >= 0)
        {
            start = false;
        }

        // Ship Rotation
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        //If collide with last collision
        if (go == _lastTriggerGo)
        {
            return;
        }
        _lastTriggerGo = go;

        //If collide with enemy
        if (go.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(go);
        }
        else
        {
            Debug.Log("Triggered by non-Enemy: " + go.name);
        }
    }

    //Get the shield level
    public float shieldLevel
    {
        get
        {
            return (_shieldLvl);
        }
        set
        {
            _shieldLvl = Mathf.Min(value, 4);

            if (value < 0)
            {
                Destroy(this.gameObject);
                ScoreManager.SCORE_MANAGER.updateHighScore();
                Main.MAIN_SINGLETON.DelayedRestart(gameRestartDelay);
            }
        }
    }
}
