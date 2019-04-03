using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [Header("Set in Inspector")]
    public AudioSource speaker;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    public void HoverSound()
    {
        speaker.PlayOneShot(hoverFX); 
    }
    public void ClickSound()
    {
        speaker.PlayOneShot(clickFX);
    }
}
