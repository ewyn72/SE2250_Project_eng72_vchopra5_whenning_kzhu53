using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        AudioManager.AUDIO_MANAGER.SwitchScene();
    }
    public void PlayGame ()
    {
        if (PlayerPrefs.GetInt("ShowedIntro", 2) != 1)
        {
            PlayerPrefs.SetInt("ShowedIntro", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

    }

    public void QuitGame ()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
