using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopProjectileMoving : MonoBehaviour
{
    private float _velocity_y;
    private float _velocity_x;

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = gameObject.GetComponent<Rigidbody>().velocity;
        //If it is moving in the y direction
        if(vel.y != 0)
        {
            //Save the current velocities
            _velocity_y = -vel.y;
            _velocity_x = -vel.x;
        }
        //When the game is paused, freeze the current positions of the bullets
        if (Pause.IS_PAUSED)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.down * 0;
            gameObject.GetComponent<Rigidbody>().velocity += Vector3.left * 0;
        }
        //When the game is not paused, move the bullets like normal
        else
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.down * _velocity_y;
            gameObject.GetComponent<Rigidbody>().velocity += Vector3.left * _velocity_x;
        }
    }
}
