using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Bindable : MonoBehaviour
{
    [SerializeField] protected AnimacioPerCodi_GameObject animacio;

    protected bool utilitzada = false;
    protected Coroutine coroutine;

    public abstract string Path { get; }
    public abstract void Activar(bool activar);
    public abstract RectTransform RectTransform { get; }

    public abstract void Restaltar();
    public abstract void Desresaltar();
    public abstract void Desactivar();
    public abstract void Actualitzar();

    protected void OnValidate() => Actualitzar();
}
