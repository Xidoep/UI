using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controls : MonoBehaviour
{
    [SerializeField] UI_Menu menu;
    System.Action hide;
    private void OnEnable()
    {
        hide = menu.ControlsHide;
    }
    public void Hide()
    {
        hide.Invoke();
    }
}
