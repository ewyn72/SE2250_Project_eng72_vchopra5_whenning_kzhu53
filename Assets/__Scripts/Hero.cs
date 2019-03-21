using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;

    private GameObject _lastTriggerGo = null;

    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLvl = 1;



    void Awake()
    {
        if (S == null)
        {
            S = this;
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

        // Ship Rotation
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        
        if (go == _lastTriggerGo)
        {
            return;
        }
        _lastTriggerGo = go;

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
            }

            Main.S.DelayedRestart(gameRestartDelay);
        }
    }
}
