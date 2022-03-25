using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Toggle : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void Canviar()
    {
        slider.value = slider.value == 0 ? 1 : 0;
    }
}
