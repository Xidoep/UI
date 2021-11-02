using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class UI_Boto : MonoBehaviour, IBindable
{
    [SerializeField] string path;

    public RectTransform Transform => GetComponent<RectTransform>();
    public string GetPath() => path;
    public void Activar(bool activat) { }
    
}
