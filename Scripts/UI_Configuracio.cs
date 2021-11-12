using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Configuracio : MonoBehaviour
{
    public System.Action hide;
    private void OnEnable()
    {
        hide = FindObjectOfType<UI_Menu>().ConfiguracioHide;
    }
    public void Hide()
    {
        hide.Invoke();
    }
}
