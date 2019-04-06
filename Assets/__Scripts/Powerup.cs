using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Vector2 rotMinMax = new Vector2(15, 90);
    public Vector2 driftMinMax = new Vector2(0.25f,2);
    public float lifeTime = 6f;
    public float fadeTime = 4f;

    [Header("Set Dynamically")]
    public WeaponType type;
    public GameObject capsule;
    public TextMesh letter;
    public Vector3 rotPerSecond;
    public float birthTime;

    private Rigidbody _rigid;
    private BoundsCheck _bndCheck;
    private Renderer _capsuleRend;


    //sets up the power up
    void Awake()
    {
        capsule = transform.Find("Capsule").gameObject;

        letter = GetComponent<TextMesh>();
        _rigid = GetComponent<Rigidbody>();
        _bndCheck = GetComponent<BoundsCheck>();
        _capsuleRend = capsule.GetComponent<Renderer>();

        Vector3 vel = Random.onUnitSphere;
        vel.z = 0;
        vel.Normalize();
        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        _rigid.velocity = vel;

        transform.rotation = Quaternion.identity;

        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y));

        birthTime = Time.time;
    }

    //This handles the life time of the powerup
    void Update()
    {
        capsule.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);

        float u = (Time.time - (birthTime + lifeTime)) / fadeTime;

        if (u >= 1)
        {
            Destroy(this.gameObject);
            return;
        }

        if (u > 0)
        {
            Color c = _capsuleRend.material.color;
            c.a = 1f - u;
            _capsuleRend.material.color = c;
            c = letter.color;
            c.a = 1f - (u * 0.5f);
            letter.color = c;
        }
        if (!_bndCheck.isOnScreen)
        {
            Destroy(gameObject);
        }
    }

    //sets the powerup type
    public void SetType(WeaponType wt)
    {
        WeaponDefinition def = Main.GetWeaponDefinition(wt);

        _capsuleRend.material.color = def.color;

        letter.text = def.letter;
        type = wt;
    }

    //destroys the powerup when it is absorbed by the hero
    public void AbsorbedBy(GameObject target)
    {
        Destroy(this.gameObject);
    }
}
