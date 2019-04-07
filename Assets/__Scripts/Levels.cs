using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    private static int _level = 1;
    public static void Increment()
    {
        _level++;
    }

    public static int currentLevel
    {
        get
        {
            return _level;
        }
    }
}
