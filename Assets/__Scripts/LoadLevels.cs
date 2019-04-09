using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevels : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levelCanvas;
    void Awake()
    {
        if (Levels.LEVEL_SINGLETON == null)
        {
            Instantiate(levelCanvas);
        }
    }
}
