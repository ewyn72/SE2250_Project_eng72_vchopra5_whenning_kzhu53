using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{
    public float radius = 1f;
    public float camWidth;
    public float camHeight;

    BoundsCheck bnd;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
        bnd = GetComponent<BoundsCheck>();
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        if(bnd != null && !bnd.isOnScreen && !bnd.offUp)
        {
            Destroy(gameObject);
        }
    }
}
