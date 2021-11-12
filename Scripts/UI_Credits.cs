using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Credits : MonoBehaviour
{
    public System.Action hide;
    private void OnEnable()
    {
        hide = FindObjectOfType<UI_Menu>().CreditsHide;
    }
    public void Hide()
    {
        hide.Invoke();
    }
}
