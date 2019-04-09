using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject scoreManager;

    // When script starts audio manager function is called to play music
    public void Start()
    {
        AudioManager.AUDIO_MANAGER.SwitchScene();
    }

    // Event handler for play button
    // when called will progress game into next scene
    public void PlayGame ()
    {
        if (PlayerPrefs.GetInt("ShowedIntro", 2) != 3)
        {
            PlayerPrefs.SetInt("ShowedIntro", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

    }

    // Event handler for quit game button
    // When called, application will be exited
    public void QuitGame ()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
