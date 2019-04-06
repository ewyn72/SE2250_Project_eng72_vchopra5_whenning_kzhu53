using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    static public CharacterSelect CHAR_SINGLETON;
    public bool solo = false;
    public bool luke = true;

    void Awake()
    {
        if (CHAR_SINGLETON == null)
        {
            CHAR_SINGLETON = this;
        }
        else
        {
            print("Char singleton already created.");
        }
    }

    public void ToggleLuke()
    {
        luke = true;
        solo = false;
        AudioManager.AUDIO_MANAGER.CharacterSelect("luke");
    }

    public void ToggleSolo()
    {
        solo = true;
        luke = false;
        AudioManager.AUDIO_MANAGER.CharacterSelect("han");
    }
}
