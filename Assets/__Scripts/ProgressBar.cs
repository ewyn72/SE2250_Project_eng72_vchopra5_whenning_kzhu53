using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public float maxTime; // In Seconds
    private float currTime = 0;

    private void Update()
    {
        slider.value = CalculateSliderValue();
        if (currTime < maxTime)
        {
            currTime += Time.deltaTime;
        }
    }

    float CalculateSliderValue()
    {
        return (currTime / maxTime);
    }
}
