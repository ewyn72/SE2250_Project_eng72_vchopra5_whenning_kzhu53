using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHighscore : MonoBehaviour
{
    //resets the highschore of the player
    public void resetHighscore()
    {
        ScoreManager.SCORE_MANAGER.resetHighscore();
    }
}
