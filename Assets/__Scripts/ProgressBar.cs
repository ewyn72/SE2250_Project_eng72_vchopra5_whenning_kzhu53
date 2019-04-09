using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    static public ProgressBar PROGRESS;
    public Slider slider;
    public float maxTime; // In Seconds
    private float _currTime = 0;
    public bool finish = false;

    void Awake()
    {
        if (PROGRESS == null)
        {
            PROGRESS = this;
        }
        else
        {
            Debug.LogError("ProgressBar.Awake() - Attempted to assign second PROGRESS.S");
        }
    }

    // Property for time
    public float CurrentTime {
        get{
            return _currTime;
        }
        set
        {
            _currTime = value;
            if(value == 0)
            {
                finish = false;
            }
        }
    }

    // Updates slider position according to current time relative to max time
    private void Update()
    {
        if (!Pause.gamePaused)
        {
            slider.value = CalculateSliderValue();
            if (_currTime < maxTime)
            {
                _currTime += Time.deltaTime;
            }
            else if (_currTime >= maxTime)
            {
                _currTime = maxTime;
                finish = true;
            }
        }
    }

    // Calculates slider value
    float CalculateSliderValue()
    {
        return (_currTime / maxTime);
    }
}
