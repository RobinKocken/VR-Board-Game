using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderPercentage : MonoBehaviour
{
    public Slider slider;

    public TMP_Text text;

    public int multiplier;

    void Update()
    {
        float v = slider.value * multiplier;

        text.text = v.ToString("f0") + "%";
    }
}
