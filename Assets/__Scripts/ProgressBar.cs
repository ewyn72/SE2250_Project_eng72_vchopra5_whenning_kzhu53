using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    static public ProgressBar PROGRESS;
    public Slider slider;
    public float maxTime; // In Seconds
    public float currTime = 0;
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

    private void Update()
    {
        slider.value = CalculateSliderValue();
        if (currTime < maxTime)
        {
            currTime += Time.deltaTime;
        }
        else if (currTime >= maxTime)
        {
            currTime = maxTime;
            finish = true;
        }
    }

    float CalculateSliderValue()
    {
        return (currTime / maxTime);
    }
}
