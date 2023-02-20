using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Seleccionar_Guardar : MonoBehaviour
{
    public static Selectable selectableGuardat;

    public void Guardar() => Guardar(gameObject.GetComponent<Selectable>());
    public void Guardar(Selectable selectable) => selectableGuardat = selectable;

    public void Carregar() => selectableGuardat.Select();
}
