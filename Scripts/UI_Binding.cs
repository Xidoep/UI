using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Binding : MonoBehaviour
{
    Input_IconePerBinding iconePerBinding;

    [Header("Referencies")]
    [SerializeField] Image degradat;
    [SerializeField] RectTransform icone;
    [SerializeField] TMP_Text etiqueta;
    [SerializeField] RectTransform final;
    [SerializeField] RectTransform linia;

    [Header("Assets")]
    [SerializeField] Sprite degradatDreta;
    [SerializeField] Sprite degradatEsquerra;

    [SerializeField] RectTransform bindable;
    bool dreta;

    GameObject liniaInici;
    GameObject liniaFinal;

    public RectTransform Bindable
    {
        set => bindable = value;
        get => bindable;
    }
    public bool Dreta
    {
        set => dreta = value;
        get => dreta;
    }

    public Input_IconePerBinding IconePerBinding => iconePerBinding;


    private void OnEnable()
    {
        if (iconePerBinding == null) iconePerBinding = GetComponent<Input_IconePerBinding>();
    }

    public void Actualitzar(Settings settings)
    {
        dreta = (bindable.anchoredPosition.x > 0);

        degradat.sprite = dreta ? degradatDreta : degradatEsquerra;

        icone.anchorMin = new Vector2(dreta ? 0 : 1, 0.5f);
        icone.anchorMax = new Vector2(dreta ? 0 : 1, 0.5f);
        icone.anchoredPosition = new Vector2(dreta ? 30 : -30, 0);

        etiqueta.margin = new Vector4(dreta ? 60 : 0, 0, dreta ? 0 : 60, 0);
        etiqueta.alignment = dreta ? TextAlignmentOptions.MidlineLeft : TextAlignmentOptions.MidlineRight;

        final.anchorMin = new Vector2(dreta ? 0 : 1, 0.5f);
        final.anchorMax = new Vector2(dreta ? 0 : 1, 0.5f);

        linia.anchorMin = new Vector2(dreta ? 0 : 1, 0.5f);
        linia.anchorMax = new Vector2(dreta ? 0 : 1, 0.5f);

        linia.transform.localScale = Vector3.zero;

        iconePerBinding.MostrarIcone();

        StartCoroutine(Linia(settings));
    }


    [ContextMenu("Linia")]
    IEnumerator Linia(Settings settings)
    {
        yield return new WaitForSecondsRealtime(0.1f);

        if (liniaInici == null) liniaInici = new GameObject("liniaInici");
        if (liniaFinal == null) liniaFinal = new GameObject("liniaFinal");
        liniaInici.transform.position = final.transform.position;
        liniaFinal.transform.position = bindable.position;

        linia.transform.position = liniaInici.transform.position;
        linia.transform.rotation = Quaternion.LookRotation(liniaInici.transform.position - liniaFinal.transform.position);
        linia.transform.localScale = new Vector3(1, 1, (liniaInici.transform.position - liniaFinal.transform.position).magnitude / settings.UISize_Get());

        Destroy(liniaInici);
        Destroy(liniaFinal);

    }
}
