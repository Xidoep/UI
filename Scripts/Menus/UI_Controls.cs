using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controls : MonoBehaviour
{
    public System.Action hide;
    private void OnEnable()
    {
        hide = FindObjectOfType<UI_Menu>().ControlsHide;
    }
    public void Hide()
    {
        hide.Invoke();
    }
}
