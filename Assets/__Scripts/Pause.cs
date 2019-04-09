using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public static bool IS_PAUSED = false;
    public GameObject pauseMenu;
    public Button setPauseButton;

    //Set the pause button as active
    public void Start()
    {
        pauseMenu.SetActive(IS_PAUSED);
        setPauseButton.onClick.AddListener(SetPause);
    }

    //Check if "p" was pressed to pause the game
    public void Update()
    {
        pauseMenu.SetActive(IS_PAUSED);
        if (Input.GetKeyUp("p"))
        {
            IS_PAUSED = !IS_PAUSED;
        }
    }

    //Reverse the state of the pause
    public static void SetPause()
    {
        IS_PAUSED = !IS_PAUSED;
    }

}
