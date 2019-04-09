using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
//base class of a weapon
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter;
    public Color color = Color.white;
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
    public float damageOnHit = 0;
    public float continuousDamage = 0;
    public float delayBetweenShots = 0;
    public float velocity = 20;
}

public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;

    [Header("Set Dynamically")]     [SerializeField]
    private WeaponType _type = WeaponType.none;
    public WeaponDefinition weaponDef;
    public GameObject collar;
    public float lastShotTime;
    private Renderer _collarRend;

    //sets up the weapons
    void Start()
    {
        collar = transform.Find("Collar").gameObject;
        _collarRend = collar.GetComponent<Renderer>();

        SetType(_type);

        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }

        GameObject rootGo = transform.root.gameObject;
        if (rootGo.GetComponent<Hero>() != null)
        {
            rootGo.GetComponent<Hero>().fireDelegate += Fire;
        }
    }

    //Get weapon type
    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }

    //Set weapon type
    public void SetType(WeaponType wt)
    {
        _type = wt;
        if (type == WeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        weaponDef = Main.GetWeaponDefinition(_type);
        _collarRend.material.color = weaponDef.color;
        lastShotTime = 0;
    }

    //Fire a projectile
    public void Fire()
    {
        if (!gameObject.activeInHierarchy) return;

        if (Time.time - lastShotTime < weaponDef.delayBetweenShots)
        {
            return;
        }
        Projectile projectile;
        Vector3 vel = Vector3.up * weaponDef.velocity;

        if (transform.up.y < 0)
        {
            vel.y = -vel.y;
        }

        switch (type)
        {
            //The blaster (normal) type of weapon
            case WeaponType.blaster:
                projectile = MakeProjectile();
                projectile.rigid.velocity = vel;
                AudioManager.AUDIO_MANAGER.Shoot("normal");
                break;

            //The spread weapon, you need to make three for this to work
            case WeaponType.spread:
                projectile = MakeProjectile();
                projectile.rigid.velocity = vel;
                projectile = MakeProjectile();
                projectile.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                projectile.rigid.velocity = projectile.transform.rotation * vel;
                projectile = MakeProjectile();
                projectile.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                projectile.rigid.velocity = projectile.transform.rotation * vel;
                AudioManager.AUDIO_MANAGER.Shoot("spread");
                break;
        }
    }

    //Make a projectile
    public Projectile MakeProjectile()
    {
        GameObject go = Instantiate<GameObject>(weaponDef.projectilePrefab);
        if ( transform.parent.gameObject.tag == "Hero")
        {
            go.tag = "ProjectileHero";
            go.layer = LayerMask.NameToLayer("ProjectileHero");
        }
        else
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }

        //Gets the transform of the position
        go.transform.position = collar.transform.position;
        go.transform.SetParent(PROJECTILE_ANCHOR, true);
        Projectile projectile = go.GetComponent<Projectile>();
        projectile.type = type;
        lastShotTime = Time.time;

        return projectile;
    }

    //handles the switching of weapons
    void Update()
    {
        //You can only switch if the game is not paused
        if (!Pause.IS_PAUSED)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) == true)
            {
                if (type == WeaponType.blaster)
                {
                    type = WeaponType.spread;
                }
                else if (type == WeaponType.spread)
                {
                    type = WeaponType.blaster;
                }
            }
        }
    }
}
