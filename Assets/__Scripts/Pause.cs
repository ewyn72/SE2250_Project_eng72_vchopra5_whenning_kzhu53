using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private static bool _isPaused = false;
    public GameObject pauseMenu;
    public Button setPauseButton;

    public void Start()
    {
        pauseMenu.SetActive(_isPaused);
        setPauseButton.onClick.AddListener(SetPause);
    }

    public void Update()
    {
        pauseMenu.SetActive(_isPaused);
        if (Input.GetKeyUp("p"))
        {
            _isPaused = !_isPaused;
        }
    }

    public static void SetPause()
    {
        _isPaused = !_isPaused;
    }

    public static bool gamePaused
    {
        get
        {
            return _isPaused;
        }
    }
}
