using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBindable
{
    public RectTransform RectTransform { get; }
    public string GetPath();
    public void Activar(bool activat);

    public void Restaltar();
    public void Desresaltar();
}
