using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization;

public class Menu_Controls : MonoBehaviour
{
    const string KEY_UI_INPUT_SELECCIONAT = "UiInputSeleccionat";

    [SerializeField] ContentElement[] botons;
    [SerializeField] AnimacioPerCodi_GameObject_Referencia[] menus;
    [SerializeField] AnimacioPerCodi_GameObject dreta;
    [SerializeField] AnimacioPerCodi_GameObject esquerra;

    //[SerializeField] XS_ScrollRect scrollRect;
    //[SerializeField] List<XS_Text> botons;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] SavableVariable<int> inputSeleccionat;
    [Apartat("Reset")]
    [SerializeField] Utils_InstantiableFromProject popupRestore;
    [SerializeField] Input_Bindings inputBindings;
    
    Lector lector;

    public void Control(int index)
    {
        Debug.Log("index " + index);
        if (index == inputSeleccionat.Valor)
            return;
        if (lector == null) lector = rectTransform.gameObject.AddComponent<Lector>();
        rectTransform.SetupAndPlay(lector, new Animacio_RectPosicio(rectTransform.anchoredPosition, new Vector2(index * -350, 0), Corba.EasyInEasyOut, true),0.25f, Transicio.clamp);

        menus[inputSeleccionat.Valor].SetAnimacio(index > inputSeleccionat.Valor ? dreta : esquerra);
        menus[inputSeleccionat.Valor].Destroy();

        inputSeleccionat.Valor = index;
        
        menus[index].SetAnimacio(index > inputSeleccionat.Valor ? dreta : esquerra);
        menus[index].gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].gameObject.SetActive(false);
        }

        botons[inputSeleccionat.Valor].GetComponent<XS_Button>().Select();

        menus[inputSeleccionat.Valor].gameObject.SetActive(true);
        Control(inputSeleccionat.Valor);

        /*for (int i = 0; i < botons.Length; i++)
        {
            botons[i].Setup(i, Control);
        }*/
        
    }

    public void PopupResetControls() => popupRestore.InstantiateReturn().GetComponent<Utils_EsdevenimentDelegat>().Registrar(ResetControls);
    void ResetControls()
    {
        inputBindings.ResetBindings();
        menus[inputSeleccionat.Valor].GetComponent<UI_Bindings>().MostrarBindings();
    }
    private void OnValidate()
    {
        inputSeleccionat = new SavableVariable<int>(KEY_UI_INPUT_SELECCIONAT, Guardat.Direccio.Local, 0);
        if (inputBindings == null) inputBindings = XS_Utils.XS_Editor.LoadAssetAtPath<Input_Bindings>("Assets/XidoStudio/Inputs/Rebindings/Bindings.asset");
    }
}