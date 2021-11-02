using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PopupIdioma : UI_Popup
{
    [SerializeField] Localitzacio localitzacio;
    int idioma;

    public void SetIdioma(int idioma) => this.idioma = idioma;

    void OnEnable()
    {
        acceptar.onClick.AddListener(() => localitzacio.IdiomaActual(idioma));
    }

    void OnDisable()
    {
        acceptar.onClick.RemoveAllListeners();
        lastSelected.Select();
    }
}
