using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButtonClicks : MonoBehaviour
{
    public Button resumeButton, muteAudioButton, mainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        muteAudioButton.onClick.AddListener(MuteAudio);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void ResumeGame()
    {
        Pause.SetPause();
    }

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

    void GoToMainMenu()
    {
        ScoreManager.SCORE_MANAGER.updateHighScore();
        Destroy(ScoreManager.SCORE_MANAGER.gameObject); // Destroy current ScoreManager
        SceneManager.LoadScene("_MainMenu");
    }
}

