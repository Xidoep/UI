using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This is used to eventually disselect this element and reselect it again later.
/// </summary>
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
        if (!selectable)
            return;

        selectable.Select();
        //selectable = null;
    }
       
}
