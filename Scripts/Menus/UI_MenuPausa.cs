using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuPausa : MonoBehaviour
{
    [SerializeField] UI_Menu menu;
    public System.Action hide;
    public System.Action credits;
    public System.Action controls;
    public System.Action configuracio;
    private void OnEnable()
    {
        hide = menu.MenuPausaHide;
        credits = menu.CreditsShow;
        controls = menu.ControlsShow;
        configuracio = menu.ConfiguracioShow;
    }
    public void Hide() => hide.Invoke();
    public void Credits() => credits.Invoke();
    public void Controls() => controls.Invoke();
    public void Configuracio() => configuracio.Invoke();
}
