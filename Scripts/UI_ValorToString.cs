using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ValorToString : MonoBehaviour
{
    public enum Visualization
    {
        Float,
        Int,
        Percent
    }

    [SerializeField] Slider slider;
    [SerializeField] Visualization visualization;
    TMP_Text text;
    [SerializeField] string sufix;

    private void OnEnable()
    {
        text = GetComponent<TMP_Text>();
        slider.onValueChanged.AddListener(Visualize);
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(Visualize);
    }


    void Visualize(float valor)
    {
        switch (visualization)
        {
            case Visualization.Float:
                text.text = $"{valor.ToString("0.#")}{sufix}";
                break;
            case Visualization.Int:
                text.text = $"{valor.ToString("0")}{sufix}";
                break;
            case Visualization.Percent:
                text.text = $"{(valor * 100).ToString("#00")}%";
                break;
        }
    }

}
