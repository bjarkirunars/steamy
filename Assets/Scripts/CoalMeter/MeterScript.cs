using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        // Debug.Log(slider.normalizedValue);
        // if (slider.value < 0.25 * GameManager.instance.maxCoals)
        // {
        //     fill.color = gradient.Evaluate(0f);
        // } else if (slider.value < 0.5 * GameManager.instance.maxCoals)
        // {
        //     fill.color = gradient.Evaluate(1f);
        // } else
        // {
        //     fill.color = gradient.Evaluate(2f);
        // }
    }

}
