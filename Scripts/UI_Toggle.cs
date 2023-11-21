using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Toggle : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] XS_Button button;


    void OnEnable()
    {
        button.onClick.AddListener(Canviar);
    }

    public void Canviar()
    {
        slider.value = slider.value == 0 ? 1 : 0;

    }
}
