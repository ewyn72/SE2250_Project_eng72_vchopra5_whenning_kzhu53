﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //variable for the rotation speed of the shield
    [Header("Set in Inspector")]
    public float rotationsPerSecond = 0.1f;

    //variable for the level of the shield
    [Header("Set Dynamically")]
    public int levelShown = 0;

    Material mat;

    //renders in the shield
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    //Update the shield level
    void Update()
    {
        //Only update if it is not paused
        if (!Pause.IS_PAUSED)
        {
            int currLevel = Mathf.FloorToInt(Hero.PLAYER_HERO.shieldLevel);
            if (levelShown != currLevel)
            {
                levelShown = currLevel;
                mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
            }

            float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
            transform.rotation = Quaternion.Euler(0, 0, rZ);
        }
    }
}
