using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBindable
{
    public string GetPath();
    public void Activar(bool activat);
    public RectTransform Transform { get; }
}
