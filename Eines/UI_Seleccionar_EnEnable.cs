using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Seleccionar_EnEnable : MonoBehaviour
{
    Selectable selectable;
    private void OnEnable()
    {
        if (selectable == null) selectable = GetComponent<Selectable>();
        StartCoroutine(Seleccionar());
    }

    IEnumerator Seleccionar()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        selectable.Select();
        
    }
}
