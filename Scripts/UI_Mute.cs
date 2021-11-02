using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Mute : MonoBehaviour
{
    [SerializeField] Slider slider;
    float rememberedValue = 1;

    public void Mute(bool mute)
    {
        if (mute)
        {
            rememberedValue = slider.value;
            slider.value = 0;
        }
        else
        {
            slider.value = rememberedValue;
        }
    }
}
