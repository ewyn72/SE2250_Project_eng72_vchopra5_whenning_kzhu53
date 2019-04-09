using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    //Private variables to set the text
    private int _level = 1;
    private string[] _listOfPlanets = { "Abafar", "Alderaan", "Batuu", "Coruscant", "D'Qar", "Eadu", "Hoth", "Iego"};
    public Text firstLevelField;
    public Text levelField;
    private float _currTime= 0f;
    private bool _showedLevel = false;

    public static Levels LEVEL_SINGLETON;

    //Create the Levels class
    private void Awake()
    {
        if(LEVEL_SINGLETON == null)
        {
            LEVEL_SINGLETON = this;
            firstLevelField.enabled = false;
            levelField = firstLevelField;
            DontDestroyOnLoad(gameObject);
        }
    }


    //Increment the level by one
    public void Increment()
    {
        _level++;
    }

    //This is so that other classes can access the level number
    public int currentLevel
    {
        get
        {
            return _level;
        }
    }

    //Reset the level and the max time per level
    public void resetLevel()
    {
       _level = 1;
        ProgressBar.PROGRESS.maxTime = 10;
    }


    public void ShowLevel()
    {
        //Show the level and set the text to white
        _showedLevel = true;
        levelField.color = Color.white;

        //Level 1 has a custom level
        if (_level == 1)
        {
            levelField.text = "Level 1: Training";
        }
        //Otherwise use a planet name that's coming up with the current level
        else
        {
            levelField.text = "Level " + currentLevel + ": " + _listOfPlanets[(currentLevel - 2) % _listOfPlanets.Length];
        }

        //Enabled the text field
        levelField.enabled = true;

        //Set the current time to 0
        _currTime = 0;

    }

    public void Update()
    {
        _currTime += Time.deltaTime;
        //If the current time is greater than 2 seconds
        if(_showedLevel && _currTime > 2.0f)
        {
            //Set the current time to 0, and set _showedLevel = false, as the level is no longer showed
            _currTime = 0f;
            _showedLevel = false;
            levelField.enabled = false;
        }
    }

}