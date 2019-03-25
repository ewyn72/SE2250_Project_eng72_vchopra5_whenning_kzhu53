using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{
    public float radius = 1f;
    public float camWidth;
    public float camHeight;

    private BoundsCheck _bnd;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
        _bnd = GetComponent<BoundsCheck>();
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        if(_bnd != null && !_bnd.isOnScreen && !_bnd.offUp)
        {
            Destroy(gameObject);
        }
    }
}
