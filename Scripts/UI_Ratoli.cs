using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class UI_Ratoli : UI_Bindable, IBindable
{
    [SerializeField] MouseButton boto;

    Image image;


    public RectTransform RectTransform => GetComponent<RectTransform>();

    public string Path => "<Mouse>/" + boto.ToString().ToLower() + "Button";
    public void Activar(bool activat)
    {
        utilitzada = activat;
        Actualitzar();
    }
    public void Desactivar() { }

    void Actualitzar()
    {
        if (!image) image = GetComponentInChildren<Image>();

        if (!image)
            return;

        image.color = utilitzada ? Color.white : new Color(.65f, .7f, .75f);
    }
    public void Restaltar() => animacio.OnPointerEnter(image, coroutine);
    public void Desresaltar() => animacio.OnPointerExit(image, coroutine);


    private void OnValidate() => Actualitzar();
}
