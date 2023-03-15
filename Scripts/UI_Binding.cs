using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Samples.RebindUI;
public class UI_Binding : MonoBehaviour
{
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

    [Apartat("Altres Referencies")]
    [SerializeField] RebindActionUI rebindActionUI;
    [SerializeField] List<InputBinding> bindings;

    [Apartat("Options")]
    [SerializeField] bool dreta;
    [SerializeField] bool overrided;
    [SerializeField] bool rebindable; //Que pots clicar per rebindar i recuperar
    [SerializeField] bool prioritzarMouse;

    [SerializeField] List<UI_Bindable> bindables;


    public void AddBindable(UI_Bindable bindable)
    {
        if (bindables == null) bindables = new List<UI_Bindable>();

        bindables.Add(bindable);
        button.OnEnter += bindable.Restaltar;
        button.OnExit += bindable.Desresaltar;
    }
    public void RemoveBindables()
    {
        for (int i = 0; i < bindables.Count; i++)
        {
            button.OnEnter -= bindables[i].Restaltar;
            button.OnExit -= bindables[i].Desresaltar;
            bindables[i].Activar(false);
            bindables[i].Desactivar();
        }
        bindables.Clear();
    }

    public UI_Bindable GetBindable => bindables[0];
    public bool Dreta { get => dreta; set => dreta = value; }

    public Input_IconePerBinding IconePerBinding => icone;

    public bool Rebindable { set => rebindable = value; }


    [ContextMenu("Actualitzar")]
    public void Actualitzar()
    {
        //Triar si ha d'anar a la dreta o a l'esquerra.
        if (Application.isPlaying)
            dreta = (bindables[bindables.Count - 1].RectTransform.anchoredPosition.x > 0);

        //Setup overrided
        bool algunBindingOverrided = false;
        for (int i = 0; i < icone.Icones.Count; i++)
        {
            if (icone.Icones[i].overrided)
            {
                algunBindingOverrided = true;
                break;
            }
        }
        overrided = algunBindingOverrided;

        //HAbilitar botons
        rectTransform.sizeDelta = new Vector2(rebindable ? 600 : 300, 50);

        button.Interactable(rebindable);
        Navigation navigation = new Navigation();
        navigation.mode = rebindable ? Navigation.Mode.Automatic : Navigation.Mode.None;
        button.navigation = navigation;

        recuperar.gameObject.SetActive(rebindable && overrided);

        //Modificar UI basica
        rectTransform.pivot = new Vector2((dreta ? 0.1f : 0.9f), 0.5f);
        rectTransform.localScale = Vector3.one;
        degradat.sprite = dreta ? degradatDreta : degradatEsquerra;
        layoutGroup.childAlignment = dreta ? TextAnchor.MiddleLeft : TextAnchor.MiddleRight;

        if (dreta)
        {
            final.SetAsFirstSibling();

            ((RectTransform)etiqueta.transform).SetAsLastSibling();
            recuperar.transform.SetAsLastSibling();
        }
        else
        {
            ((RectTransform)etiqueta.transform).SetAsFirstSibling();
            recuperar.transform.SetAsFirstSibling();

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

    }

    public void MostrarIcone()
    {
        icone.MostrarIcone(rebindActionUI.actionReference, true);
        if (rebindable && overrided)
            recuperarIcone.MostrarIcone(rebindActionUI.actionReference, true);
    }


    private void OnValidate()
    {
        if (button == null) button = GetComponent<XS_Button>();
        if (icone == null) icone = GetComponent<Input_IconePerBinding>();

        icone.SetInputActionReference = rebindActionUI.actionReference;
        recuperarIcone.SetInputActionReference = rebindActionUI.actionReference;

        icone.SetPropritzarMouse = prioritzarMouse;
        recuperarIcone.SetPropritzarMouse = prioritzarMouse;
        //icone.SetInputActionReference(null);
        //recuperarIcone.SetInputActionReference(null);
    }
}
