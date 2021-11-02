using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopupExit : UI_Popup
{
    void OnEnable()
    {
        acceptar.onClick.AddListener(() => Application.Quit());
    }

    void OnDisable()
    {
        acceptar.onClick.RemoveAllListeners();
        lastSelected.Select();
    }
}
