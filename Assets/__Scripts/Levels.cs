using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    private static int _level = 0;
    public static void Increment()
    {
        _level++;
    }
}
