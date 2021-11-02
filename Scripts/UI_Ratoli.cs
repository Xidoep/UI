using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class UI_Ratoli : MonoBehaviour, IBindable
{
    Image image;
    [SerializeField] bool utilitzada = false;
    [SerializeField] MouseButton boto;

    public RectTransform Transform => GetComponent<RectTransform>();

    [ContextMenu("Provar")]
    public string GetPath() => "<Mouse>/" + boto.ToString().ToLower() + "Button";
    public void Activar(bool activat)
    {
        utilitzada = activat;
        Actualitzar();
    }

    private void OnValidate()
    {
        Actualitzar();
    }

    void Actualitzar()
    {
        if (!image) image = GetComponentInChildren<Image>();

        if (!image)
            return;

        image.color = utilitzada ? Color.white : new Color(.65f, .7f, .75f);
    }
}
