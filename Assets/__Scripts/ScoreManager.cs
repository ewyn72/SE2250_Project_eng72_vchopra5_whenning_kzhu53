using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static public ScoreManager SCORE_MANAGER; //Singleton
    public Text highScore;
    public Text currentScore;
    public bool isActive = false;
    private float _highS;
    private float _currS;

    //Set highscore
    void Awake()
    {
        _highS = (float) PlayerPrefs.GetInt("highscore", 0);
        highScore.text = "HighScore: " + _highS;
        _currS = 0;
        if (SCORE_MANAGER == null)
        {
            SCORE_MANAGER = this;
        }
        else
        {
            print("Score Manager already created.");
        }
        DontDestroyOnLoad(SCORE_MANAGER);
    }

    //update current score
    public void updateCurrScore(float score)
    {
        _currS += score;
        currentScore.text = "Current Score: " + _currS;
    }

    //update the high score
    public void updateHighScore()
    {
        if(_highS < _currS)
        {
            _highS = _currS;
        }
        _currS = 0;

        PlayerPrefs.SetInt("highscore", (int) _highS);
        PlayerPrefs.Save();

        highScore.text = "HighScore: " + _highS;
    }

    //reset the high score
    public void resetHighscore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        _highS = 0f;
        highScore.text = "HighScore: " + _highS;
    }

    public void SetActive(bool state)
    {
        this.SetActive(state);
    }
}
