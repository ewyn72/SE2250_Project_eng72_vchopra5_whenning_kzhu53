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

    public virtual void Move()
    {
        Vector3 position = this.transform.position;
        position.y -= speed * Time.deltaTime;
        this.transform.position = position;
    }

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
            case "ProjectileHero":
                Projectile p = otherGO.GetComponent<Projectile>();
                if (!(_bndCheck.isOnScreen))
                {
                    Destroy(otherGO);
                    break;
                }
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                ShowDamage();

                if (health <= 0)
                {
                    Destroy(this.gameObject);
                    ScoreManager.SM.updateCurrScore(score);
                }

                Destroy(otherGO);
                break;

            default:
                Debug.Log("Enemy hit by non-ProjectileHero: " + otherGO.name);
                break;
        }
    }

    void ShowDamage()
    {
        foreach(Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
}
