using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButtonClicks : MonoBehaviour
{
    //Buttons
    public Button resumeButton, muteAudioButton, mainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        //Add the listeners
        resumeButton.onClick.AddListener(ResumeGame);
        muteAudioButton.onClick.AddListener(MuteAudio);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }


    //Resume the game
    void ResumeGame()
    {
        Pause.SetPause();
    }


    //Depending on whether or not it has been muted, set the text to either mute/unmute
    void MuteAudio()
    {
        int ret = AudioManager.AUDIO_MANAGER.Mute();
        if (ret == 1)
        {
            muteAudioButton.GetComponentInChildren<Text>().text = "Mute Audio";
        }
        else {
            muteAudioButton.GetComponentInChildren<Text>().text = "Unmute Audio";
        }


    }

    //Go to the main menu (reset the game)
    void GoToMainMenu()
    {
        ScoreManager.SCORE_MANAGER.updateHighScore();
        Destroy(ScoreManager.SCORE_MANAGER.gameObject); // Destroy current ScoreManager
        Levels.LEVEL_SINGLETON.resetLevel();
        Pause.SetPause();
        SceneManager.LoadScene("_MainMenu");
    }
}

