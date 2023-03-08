using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Binding : MonoBehaviour
{
    [SerializeField] XS_Button button;
    [SerializeField] Input_IconePerBinding iconePerBinding;

    [Header("Referencies")]
    [SerializeField] Image degradat;
    [SerializeField] RectTransform icone;
    [SerializeField] TMP_Text etiqueta;
    [SerializeField] RectTransform final;
    [SerializeField] RectTransform linia;

    [Header("Assets")]
    [SerializeField] Sprite degradatDreta;
    [SerializeField] Sprite degradatEsquerra;

    [Header("Options")]
    [SerializeField] bool nonChangable = false;

    //IBindable bindable;
    List<IBindable> bindables;
    [SerializeField] bool dreta;
    bool registrat;

    GameObject liniaInici;
    GameObject liniaFinal;



    /*public IBindable Bindable { get => bindable;
        set { 
            bindable = value;
            if (!registrat)
            {
                registrat = true;
                button.OnEnter += bindable.Restaltar;
                button.OnExit += bindable.Desresaltar;
            }
        } 
    }*/
    public void AddBindable(IBindable bindable)
    {
        if (bindables == null) bindables = new List<IBindable>();

        bindables.Add(bindable);
        button.OnEnter += bindable.Restaltar;
        button.OnExit += bindable.Desresaltar;
    }
    public IBindable GetBindable => bindables[0];
    public bool Dreta { get => dreta; set => dreta = value; }

    public Input_IconePerBinding IconePerBinding => iconePerBinding;


    private void OnEnable()
    {
        if (nonChangable)
        {
            degradat.color = Color.gray;
            degradat.raycastTarget = false;
            degradat.GetComponent<Button>().interactable = false;
        }
    }

    public void Actualitzar(float interfaceSize)
    {
        if (Application.isPlaying)
            dreta = (bindables[bindables.Count - 1].RectTransform.anchoredPosition.x > 0);
        
        GetComponent<RectTransform>().pivot = new Vector2((dreta ? 0.1f : 0.9f), 0.5f);

        degradat.sprite = dreta ? degradatDreta : degradatEsquerra;

        icone.anchorMin = new Vector2(dreta ? 0 : 1, 0.5f);
        icone.anchorMax = new Vector2(dreta ? 0 : 1, 0.5f);
        icone.anchoredPosition = new Vector2(dreta ? 30 : -30, 0);

        etiqueta.margin = new Vector4(dreta ? 60 : 0, 0, dreta ? 0 : 60, 0);
        etiqueta.alignment = dreta ? TextAlignmentOptions.MidlineLeft : TextAlignmentOptions.MidlineRight;

        linia.gameObject.SetActive(false);

        final.anchorMin = new Vector2(dreta ? 0 : 1, 0.5f);
        final.anchorMax = new Vector2(dreta ? 0 : 1, 0.5f);

        //linia.anchorMin = new Vector2(dreta ? 0 : 1, 0.5f);
        //linia.anchorMax = new Vector2(dreta ? 0 : 1, 0.5f);

        //linia.transform.localScale = Vector3.zero;

        iconePerBinding.MostrarIcone();

        if (!Application.isPlaying)
            return;

        StartCoroutine(Linia(interfaceSize));
    }


    [ContextMenu("Linia")]
    IEnumerator Linia(float interfaceSize)
    {
        yield return new WaitForSecondsRealtime(0.1f);

        if (liniaInici == null) liniaInici = new GameObject("liniaInici");
        if (liniaFinal == null) liniaFinal = new GameObject("liniaFinal");
        liniaInici.transform.position = final.transform.position;
        //liniaFinal.transform.position = bindables[0].RectTransform.position;

        //linia.transform.position = liniaInici.transform.position;
        //linia.transform.rotation = Quaternion.LookRotation(liniaInici.transform.position - liniaFinal.transform.position);
        //linia.transform.localScale = new Vector3(1, 1, (liniaInici.transform.position - liniaFinal.transform.position).magnitude / interfaceSize);

        Destroy(liniaInici);
        Destroy(liniaFinal);

    }

    private void OnValidate()
    {
        if (button == null) button = GetComponent<XS_Button>();
        if (iconePerBinding == null) iconePerBinding = GetComponent<Input_IconePerBinding>();
        Actualitzar(1);
    }
}
