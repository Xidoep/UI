using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_PopupToMainMenu : UI_Popup
{
    [SerializeField] UnityEvent escenaMainMenu;

    void OnEnable()
    {
        acceptar.onClick.AddListener(() => escenaMainMenu.Invoke());
    }

    void OnDisable()
    {
        acceptar.onClick.RemoveAllListeners();
        lastSelected.Select();
    }
}
