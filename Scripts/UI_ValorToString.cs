using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_ValorToString : MonoBehaviour
{
    TMP_Text text;
    TMP_Text Text
    {
        get
        {
            if (text == null) text = GetComponent<TMP_Text>();
            return text;
        }
    }
    public void SetValor(float valor) => Text.text = valor.ToString("0.#");
    public void SetValor(int valor) => Text.text = valor.ToString();
    public void SetValorPerCent(float valor) => Text.text = $"{(valor * 100).ToString("#00")}%";
}
