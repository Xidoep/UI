using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slider : MonoBehaviour
{
    Slider slider;
    [SerializeField] float quantitat;

    private void OnEnable()
    {
        if (slider == null) slider = GetComponent<Slider>();
    }

    public void Augmentar() => slider.value += quantitat;
    public void Disminuir() => slider.value -= quantitat;
}
