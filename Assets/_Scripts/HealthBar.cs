using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void MaxHealth(int Health)
    {
        slider.maxValue = Health;
        slider.value = Health;
    }
    public void SetHealth(int Health)
    {
        slider.value = Health;
    }
}
