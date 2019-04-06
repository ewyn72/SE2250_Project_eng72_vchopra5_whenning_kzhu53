using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private BoundsCheck _bndCheck;
    public float speed = 10f;
    public float score = 100f;
    public float health = 2f;
    public float showDamageDuration = 0.1f;

    public float radius = 1f;

    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage = false;
    public float damageDoneTime;
    public bool notifiedOfDestruction = false;

    //Set the enemy color
    void Awake()
    {
        _bndCheck = GetComponent<BoundsCheck>();

        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i=0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    //Move the enemy
    public virtual void Move()
    {
        Vector3 position = this.transform.position;
        position.y -= speed * Time.deltaTime;
        this.transform.position = position;
    }

    //Every frame, move the enemy and remove the red colour if needed
    public void Update()
    {
        Move();

        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }
    }

   
    void OnCollisionEnter( Collision coll )
    {
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            //If it collides with the projectile, subtract health. If health < 0, destroy
            case "ProjectileHero":
                Projectile projectile = otherGO.GetComponent<Projectile>();
                if (!(_bndCheck.isOnScreen))
                {
                    Destroy(otherGO);
                    break;
                }
                health -= Main.GetWeaponDefinition(projectile.type).damageOnHit;
                ShowDamage();

                if (health <= 0)
                {
                    Destroy(this.gameObject);
                    ScoreManager.SCORE_MANAGER.updateCurrScore(score);
                }

                Destroy(otherGO);
                break;
                //If it collides with the non-projectile, then print debug message
            default:
                Debug.Log("Enemy hit by non-ProjectileHero: " + otherGO.name);
                break;
        }
    }

    //Switch color to red for a given time
    void ShowDamage()
    {
        foreach(Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    //Switch color to original
    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
}
