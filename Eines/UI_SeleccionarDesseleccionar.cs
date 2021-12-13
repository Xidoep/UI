using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SeleccionarDesseleccionar : MonoBehaviour
{
    Selectable selectable;

    public void DesseleccionarIRecordar(Selectable selectable)
    {
        this.selectable = selectable;
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void Recuperar()
    {
        selectable.Select();
        selectable = null;
    }
       
}
