using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

[SelectionBase]
public class UI_Tecla : MonoBehaviour, IBindable
{
    public Image[] image;
    public TMP_Text text;
    [SerializeField] bool utilitzada = false;
    [SerializeField] Key tecla;

    public Key Tecla => tecla;

    public RectTransform Transform => GetComponent<RectTransform>();
    Image[] Image
    {
        get
        {
            if (image.Length == 0) image = GetComponentsInChildren<Image>();
            return image;
        }
    }
    public TMP_Text Text
    {
        get
        {
            if (!text) text = GetComponentInChildren<TMP_Text>();
            return text;
        }
    }

    [ContextMenu("Provar")]
    public string GetPath() => "<Keyboard>/" + tecla.ToString().Substring(0, 1).ToLower() + tecla.ToString().Substring(1);
    public void Activar(bool activat)
    {
        utilitzada = activat;
        Actualitzar();
    }
    private void OnValidate() => Actualitzar();

    public void Actualitzar()
    {
        if(image == null)
        {
            image = new Image[0];
            if (image.Length == 0) image = GetComponentsInChildren<Image>(true);
        }
        if (!text) text = GetComponentInChildren<TMP_Text>();

        if(text)
            text.text = TeclaNom();

        if(image.Length > 0)
        {
            for (int i = 0; i < image.Length; i++)
            {
                image[i].color = utilitzada ? Color.white : new Color(.65f, .7f, .75f);
            }
        }
        
        


    }
    string TeclaNom()
    {
        if (tecla.ToString().Contains("Divide")) return "/";
        if (tecla.ToString().Contains("Multiply")) return "*";
        if (tecla.ToString().Contains("Minus")) return "-";
        if (tecla.ToString().Contains("Plus")) return "+";
        if (tecla.ToString().Contains("Period")) return ".";
        if (tecla.ToString().Contains("Numpad")) return tecla.ToString().Substring(6);
        if (tecla.ToString().Contains("Digit")) return tecla.ToString().Substring(5);
        if (tecla.ToString().Equals("Escape")) return "Esc";
        if (tecla.ToString().Equals("Insert")) return "Ins";
        if (tecla.ToString().Equals("Delete")) return "Del";
        if (tecla.ToString().Equals("PageUp")) return "PUp";
        if (tecla.ToString().Equals("PageDown")) return "PDown";
        if (tecla.ToString().Contains("CapsLock")) return "Caps";
        if (tecla.ToString().Contains("Shift")) return "Shift";
        if (tecla.ToString().Contains("Ctrl")) return "Ctrl";
        if (tecla.ToString().Contains("Alt")) return "Alt";
        if (tecla.ToString().Contains("Arrow")) return "";
        if (tecla.ToString().Equals("None")) return "";
        return tecla.ToString();
    }
}
