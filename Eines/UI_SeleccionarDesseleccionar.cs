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
    public static Selectable selectableGuardat;

    public void DesseleccionarIRecordar() => DesseleccionarIRecordar(gameObject.GetComponent<Selectable>());
    public void DesseleccionarIRecordar(Selectable selectable)
    {
        selectable.OnDeselect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        selectableGuardat = selectable;
    }
    public void Recuperar()
    {
        if (!selectableGuardat)
            return;

        selectableGuardat.Select();
        //selectable = null;
    }
       
}
