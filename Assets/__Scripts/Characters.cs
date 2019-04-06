using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public GameObject[] characterList;

    // Start is called before the first frame update
    void Start()
    {
        characterList = new GameObject[transform.childCount];

        // Fill the array with our models
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        if(CharacterSelect.CHAR_SINGLETON.luke)
        {
            Instantiate<GameObject>(characterList[0]);
        }
        else
        {
            Instantiate<GameObject>(characterList[1]);
        }
    }

}
