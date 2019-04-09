using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevels : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levelCanvas;
    void Awake()
    {
        //Check if level text exists, if it doesn't then create it
        if (Levels.LEVEL_SINGLETON == null)
        {
            Instantiate(levelCanvas);
        }
    }
}
