using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static public ScoreManager SM;
    public Text highScore;
    public Text currentScore;
    private float highS;
    private float currS;

    void Awake()
    {
        highS = (float) PlayerPrefs.GetInt("highscore", 0);
        highScore.text = "HighScore: " + highS;
        currS = 0;
        SM = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCurrScore(float score)
    {
        currS += score;
        currentScore.text = "Current Score: " + currS;
    }

    public void updateHighScore()
    {
        if(highS < currS)
        {
            highS = currS;
        }
        currS = 0;

        PlayerPrefs.SetInt("highscore", (int) highS);
        PlayerPrefs.Save();

        highScore.text = "HighScore: " + highS;
    }

    public void resetHighscore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        highS = 0f;
        highScore.text = "HighScore: " + highS;
    }
}
