using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class UI_Boto : UI_Bindable
{
    [SerializeField] string path;
    [SerializeField] Image[] image;


    public override RectTransform RectTransform => GetComponent<RectTransform>();
    public override string Path => path;
    public override void Activar(bool activat)
    {
        utilitzada = activat;
        Actualitzar();
    }
    public override void Actualitzar()
    {
        if (image == null)
        {
            image = new Image[0];
            if (image.Length == 0) image = GetComponentsInChildren<Image>(true);
        }

        if (image.Length > 0)
        {
            for (int i = 0; i < image.Length; i++)
            {
                image[i].color = utilitzada ? Color.white : new Color(.65f, .7f, .75f);
            }
        }
    }

    public override void Restaltar() 
    {
        for (int i = 0; i < image.Length; i++)
        {
            coroutine = animacio.OnPointerEnter(image[i], coroutine);
        }
    }
    public override void Desresaltar()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        for (int i = 0; i < image.Length; i++)
        {
            coroutine = animacio.OnPointerExit(image[i], coroutine);
        }
    }
    public override void Desactivar()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        for (int i = 0; i < image.Length; i++)
        {
            animacio.Disable(image[i], ref coroutine, false);
        }
    }


}
