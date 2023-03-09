using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Binding : MonoBehaviour
{

    [Header("Base")]
    [SerializeField] XS_Button button;
    [SerializeField] HorizontalLayoutGroup layoutGroup;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image degradat;

    [Apartat("Elements del boto")]
    [SerializeField] RectTransform final;
    [SerializeField] Input_IconePerBinding icone;
    [SerializeField] TMP_Text etiqueta;
    [Space(10)]
    [SerializeField] Sprite degradatDreta;
    [SerializeField] Sprite degradatEsquerra;

    [Apartat("Recuperar")]
    [SerializeField] RectTransform recuperar;
    [SerializeField] HorizontalLayoutGroup recuperarLayout;
    [SerializeField] Input_IconePerBinding recuperarIcone;
    [SerializeField] RectTransform recuperarTexte;

    [Apartat("Options")]
    [SerializeField] bool dreta;
    [SerializeField] bool overrided;
    [SerializeField] bool rebindable; //Que pots clicar per rebindar i recuperar

    List<IBindable> bindables;


    public void AddBindable(IBindable bindable)
    {
        if (bindables == null) bindables = new List<IBindable>();

        bindables.Add(bindable);
        button.OnEnter += bindable.Restaltar;
        button.OnExit += bindable.Desresaltar;
    }
    public IBindable GetBindable => bindables[0];
    public bool Dreta { get => dreta; set => dreta = value; }

    public Input_IconePerBinding IconePerBinding => icone;

    public bool Rebindable { set => rebindable = value; }



    public void Actualitzar()
    {

        //Triar si ha d'anar a la dreta o a l'esquerra.
        if (Application.isPlaying)
            dreta = (bindables[bindables.Count - 1].RectTransform.anchoredPosition.x > 0);

        //HAbilitar botons
        rectTransform.sizeDelta = new Vector2(rebindable ? 500 : 300, 50);

        button.Interactable(rebindable);
        Navigation navigation = new Navigation();
        navigation.mode = rebindable ? Navigation.Mode.Automatic : Navigation.Mode.None;
        button.navigation = navigation;

        recuperar.gameObject.SetActive(rebindable && overrided);

        //Modificar UI basica
        rectTransform.pivot = new Vector2((dreta ? 0.1f : 0.9f), 0.5f);
        degradat.sprite = dreta ? degradatDreta : degradatEsquerra;
        layoutGroup.childAlignment = dreta ? TextAnchor.MiddleLeft : TextAnchor.MiddleRight;
        if (dreta)
        {
            final.SetAsFirstSibling();

            ((RectTransform)etiqueta.transform).SetAsLastSibling();
            recuperar.SetAsLastSibling();
        }
        else
        {
            ((RectTransform)etiqueta.transform).SetAsFirstSibling();
            recuperar.SetAsFirstSibling();

            final.SetAsLastSibling();
        }

        //Modificar boto recover
        if (rebindable && overrided)
        {
            recuperarLayout.childAlignment = dreta ? TextAnchor.MiddleLeft : TextAnchor.MiddleRight;

            if (dreta)
            {
                recuperarTexte.SetAsLastSibling();
            }
            else
            {
                recuperarTexte.SetAsFirstSibling();
            }
        }



        //Visualitzar bindings
        if (!Application.isPlaying)
            return;

        icone.MostrarIcone();
        if (rebindable && overrided)
            recuperarIcone.MostrarIcone();
    }



    private void OnValidate()
    {
        if (button == null) button = GetComponent<XS_Button>();
        if (icone == null) icone = GetComponent<Input_IconePerBinding>();
        //Actualitzar();
    }
}
