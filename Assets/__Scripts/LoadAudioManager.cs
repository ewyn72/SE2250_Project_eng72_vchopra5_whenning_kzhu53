using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAudioManager : MonoBehaviour
{

    //The Audio Manager that is going to be created
    public GameObject AudioManagerCreate;

    //Instantiate the audio manager
    void Awake()
    {
        Instantiate(AudioManagerCreate);
    }

}
