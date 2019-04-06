using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour{ 

    public void HoverSound()
    {
        AudioManager.AUDIO_MANAGER.playHover();
    }
    public void ClickSound()
    {
        AudioManager.AUDIO_MANAGER.playClick();
    }
}
