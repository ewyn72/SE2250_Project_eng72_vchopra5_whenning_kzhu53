using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float showInvinDuration = 5f;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLvl = 1;
    private float _startInvin;
    private float _InvinTime = 5f;
    private bool _isInvincible = false;
    private bool _startAnimation = true;
    public Color[] originalColors;
    public Material[] materials;
    public bool showingInvin = false;
    public float invinDoneTime;


    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    //sets up the player hero
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

        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    //main logic and movement for the hero
    void Update()
    {
        if (!Pause.IS_PAUSED)
        {
            float timer = (Time.time - (_startInvin + _InvinTime));
            if (timer > 0)
            {
                _isInvincible = false;
            }

            // Get info from input class
            float xAxis = Input.GetAxis("Horizontal");
            float yAxis = Input.GetAxis("Vertical");

            Vector3 pos = transform.position;

            if (!_startAnimation) //Prevents user input when script moving hero
            {
                pos.x += xAxis * speed * Time.deltaTime;
                pos.y += yAxis * speed * Time.deltaTime;
                transform.position = pos;
            }

            // Cool fly-in at start
            if (pos.y >= 0 || SceneManager.GetActiveScene().buildIndex == 3)
            {
                _startAnimation = false;
            }
            if (_startAnimation && SceneManager.GetActiveScene().buildIndex == 2)
            {
                pos.y += speed * Time.deltaTime;
                transform.position = pos;
            }

            // Ship Rotation
            transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

            if (Input.GetAxis("Jump").Equals(1) && fireDelegate != null)
            {
                fireDelegate();
            }

            //turns the player back to its original colours when invincibility runs out
                if (showingInvin && Time.time > invinDoneTime)
                {
                    UnShowInvin();
                }
            
            
        }
    }

    //deals with colliding with enemies, enemy projectiles, and powerups
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
        if (go.tag == "Enemy" || go.tag == "ProjectileEnemy")
        {
            if (!_isInvincible)
            {
                shieldLevel--;
            }
                Destroy(go);
        }
        else if (go.tag == "PowerUp")
        {
            AbsorbPowerUp(go);
        }
        else if(go.tag == "ProjectileEnemy")
        {
            if (!_isInvincible)
            {
                shieldLevel--;
            }
                Destroy(go);
        }
        else if (go.tag == "EnemyBoss")
        {
            //Kills the hero by lowering to death threshold
            shieldLevel = 0;
            shieldLevel--;
        }
        else
        {
            Debug.Log("Triggered by non-Enemy: " + go.name);
        }
    }

    //logic for powerups
    public void AbsorbPowerUp(GameObject go)
    {
        Powerup pu = go.GetComponent<Powerup>();
        switch (pu.type)
        {
            //the shield powerup. It raises the hero's shield level by 1
            case WeaponType.shield:
                if (_shieldLvl < 4)
                {
                    _shieldLvl += 1;
                }
                break;
            //the invincibility powerup. Makes the hero invulnerable for a short time and turns the hero yellow for that duration.
            case WeaponType.invincibility:
                _isInvincible = true;
                _startInvin = Time.time;
                ShowInvin();
                break;
            //the nuke powerup. It destroys all enemies onscreen.
            case WeaponType.nuke:
                Main.MAIN_SINGLETON.nuke();
                break;
        }
        pu.AbsorbedBy(this.gameObject);
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
                Levels.LEVEL_SINGLETON.resetLevel();
                Main.MAIN_SINGLETON.DelayedRestart(gameRestartDelay);
            }
        }
    }

    //turns the hero yellow
    void ShowInvin()
    {
        foreach (Material m in materials)
        {
            m.color = Color.yellow;
        }
        showingInvin = true;
        invinDoneTime = Time.time + showInvinDuration;
    }

    //returns the hero to its original colour
    void UnShowInvin()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingInvin = false;
    }
}
