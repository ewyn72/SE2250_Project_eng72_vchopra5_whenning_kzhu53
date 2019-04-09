using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour{ 

    // Event handlers for button sounds

    //Called when one of the options in the main menu is hovered
    public void HoverSound()
    {
        AudioManager.AUDIO_MANAGER.PlayHover();
    }

    //Called when one of the options in the main menu is clicked
    public void ClickSound()
    {
        AudioManager.AUDIO_MANAGER.PlayClick();
    }
}
