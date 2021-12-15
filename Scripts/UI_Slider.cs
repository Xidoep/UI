using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Toggle mute;
    [SerializeField] float quantitat;
    float rememberedValue = 1;

    private void OnEnable()
    {
        if (slider.value == 0)
        {
            if (mute != null)
            {
                rememberedValue = 1;
                mute.isOn = false;
            }
        }
    }

    public void Augmentar() => slider.value += quantitat;
    public void Disminuir() => slider.value -= quantitat;

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
