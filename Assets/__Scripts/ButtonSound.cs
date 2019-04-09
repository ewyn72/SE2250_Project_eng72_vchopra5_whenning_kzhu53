using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour{ 

    // Event handlers for button sounds
    public void HoverSound()
    {
        AudioManager.AUDIO_MANAGER.PlayHover();
    }
    public void ClickSound()
    {
        AudioManager.AUDIO_MANAGER.PlayClick();
    }
}
