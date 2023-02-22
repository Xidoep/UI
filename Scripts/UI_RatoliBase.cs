using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.LowLevel;

public class UI_RatoliBase : UI_Bindable, IBindable
{
    [SerializeField] Input_MouseInteraccions interaccio;

    Image image;


    public RectTransform RectTransform => GetComponent<RectTransform>();

    [ContextMenu("Provar")]
    public string GetPath() => "<Mouse>/" + interaccio.ToString().ToLower();
    public void Activar(bool activat)
    {
        utilitzada = activat;
        Actualitzar();
    }

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
