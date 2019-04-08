using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    private static int _level = 1;
    private static string[] _listOfPlanets = { "Abafar", "Alderaan", "Batuu", "Coruscant", "D'Qar", "Eadu", "Hoth", "Iego"};
    public Text firstLevelField;
    public static Text levelField;
    private static float _currTime = 0f;
    private static bool _showedLevel = false;

    private void Awake()
    {
        firstLevelField.enabled = false;
        SetStaticVars();
    }

    private void SetStaticVars()
    {
        levelField = firstLevelField;
        DontDestroyOnLoad(gameObject);
    }

    public static void Increment()
    {
        _level++;
    }

    public static int currentLevel
    {
        get
        {
            return _level;
        }
    }

    public static void resetLevel()
    {
        _level = 1;
        ProgressBar.PROGRESS.maxTime = 10;
    }

    public static void ShowLevel()
    {
        _showedLevel = true;
        print("Showing level");
        levelField.color = Color.white;
        if (_level == 1)
        {
            levelField.text = "Level 1: Training";
        }
        else
        {
            levelField.text = "Level " + currentLevel + ": " + _listOfPlanets[(currentLevel - 2) % _listOfPlanets.Length];
        }

        levelField.enabled = true;
        _currTime = 0;


    }

    public void Update()
    {
        _currTime += Time.deltaTime;
        if(_showedLevel && _currTime > 3.0f)
        {
            _currTime = 0f;
            _showedLevel = false;
            levelField.enabled = false;
        }
    }

}
