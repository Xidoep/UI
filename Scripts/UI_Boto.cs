using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class UI_Boto : UI_Bindable, IBindable
{
    [SerializeField] string path;
    Image image;


    public RectTransform RectTransform => GetComponent<RectTransform>();
    public string Path => path;
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
