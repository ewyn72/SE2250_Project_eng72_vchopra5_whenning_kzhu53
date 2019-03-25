using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{
    public float radius = 1f;
    public float camWidth;
    public float camHeight;

    private BoundsCheck _bnd;

    //When awake set bounds
    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
        _bnd = GetComponent<BoundsCheck>();
    }

    //After everything has finished updating, check if enemy is out of bounds
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        if(_bnd != null && !_bnd.isOnScreen && !_bnd.offUp)
        {
            //If enemy is out of bounds, then destroy
            Destroy(gameObject);
        }
    }
}
