using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.LowLevel;

public class UI_RatoliBase : MonoBehaviour, IBindable
{
    Image image;
    [SerializeField] bool utilitzada = false;
    [SerializeField] Input_MouseInteraccions interaccio;

    public RectTransform Transform => GetComponent<RectTransform>();

    [ContextMenu("Provar")]
    public string GetPath() => "<Mouse>/" + interaccio.ToString().ToLower();
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
