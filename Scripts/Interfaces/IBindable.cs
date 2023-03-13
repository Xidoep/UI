using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBindable
{
    public RectTransform RectTransform { get; }
    public string Path { get; }
    public void Activar(bool activat);
    public void Desactivar();
    public void Restaltar();
    public void Desresaltar();
}
