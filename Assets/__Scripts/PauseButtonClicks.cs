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
        AudioManager.AUDIO_MANAGER.Mute();
    }

    void GoToMainMenu()
    {
        ScoreManager.SCORE_MANAGER.updateHighScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2 );
    }
}
