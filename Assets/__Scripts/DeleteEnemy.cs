using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{
    public float radius = 1f;
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        if (pos.x > camWidth - radius)
        {
            Destroy(gameObject);
        }
        if (pos.x < -camWidth + radius)
        {
            Destroy(gameObject);
        }
        if (pos.y < -camHeight + radius)
        {
            Destroy(gameObject);
        }
        transform.position = pos;
    }
}
