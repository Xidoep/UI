using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Configuracio : MonoBehaviour
{
    [SerializeField] UI_Menu menu;
    System.Action hide;
    private void OnEnable()
    {
        hide = menu.ConfiguracioHide;
    }
    public void Hide()
    {
        hide.Invoke();
    }
}
