using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplianceFunctionProgressBar : MonoBehaviour
{
    public Slider slider;
    public Image image;

    public float progress
    {
        set
        {
            slider.value = value;
        }
        get
        {
            return slider.value;
        }
    }

}
