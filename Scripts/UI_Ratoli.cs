using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class UI_Ratoli : UI_Bindable
{
    [SerializeField] MouseButton boto;

    Image image;


    public override RectTransform RectTransform => GetComponent<RectTransform>();

    public override string Path => "<Mouse>/" + boto.ToString().ToLower() + "Button";
    public override void Activar(bool activat)
    {
        utilitzada = activat;
        Actualitzar();
    }

    public override void Actualitzar()
    {
        if (!image) image = GetComponentInChildren<Image>();

        if (!image)
            return;

        image.color = utilitzada ? Color.white : new Color(.65f, .7f, .75f);
    }
    public override void Restaltar() => coroutine = animacio.OnPointerEnter(image, coroutine);
    public override void Desresaltar() => coroutine = animacio.OnPointerExit(image, coroutine);
    public override void Desactivar() => animacio.Disable(image, ref coroutine, false);
}
