﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0 : Enemy
{
    //Move enemy straight down
    public override void Move()
    {
        if (!Pause.IS_PAUSED)
        {
            base.Move();
        }
    }

}