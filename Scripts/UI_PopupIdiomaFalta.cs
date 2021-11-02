using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopupIdiomaFalta : UI_Popup
{
    [SerializeField] string url;

    void OnEnable()
    {
        acceptar.onClick.AddListener(() => Application.OpenURL(url));
    }

    void OnDisable()
    {
        acceptar.onClick.RemoveAllListeners();
        lastSelected.Select();
    }
}
