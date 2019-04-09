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
        //Only create if one doesn't exist
        if(AudioManager.AUDIO_MANAGER == null){
            Instantiate(AudioManagerCreate);
        }
    }

}
