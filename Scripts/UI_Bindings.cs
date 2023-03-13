using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Samples.RebindUI;
using XS_Utils;
using System;

public class UI_Bindings : MonoBehaviour
{
    //[SerializeField] Settings settings;
    SavableVariable<float> interfaceSize;
    [SerializeField] Input_Bindings input_Bindings;
    [SerializeField] Transform bindingsE, bindingsD;
    [SerializeField] UI_Binding[] bindings;
    [SerializeField] bool rebindable;
    [SerializeField] bool posicionar;

    List<IBindable> bindables;

    bool trobat = false;
    int index = 0;
    int mesApunt = 0;

    List<UI_Binding> trobats;
    List<UI_Binding> ordenats;
    PlayerInput playerInput;

    [ContextMenu("ApagarBindables")]
    void ApagarBindables()
    {
        for (int i = 0; i < bindings.Length; i++)
        {
            bindings[i].RemoveBindables();
        }

    }

    private void OnEnable()
    {
        
        playerInput = FindObjectOfType<PlayerInput>();

        interfaceSize = new SavableVariable<float>(UI_Tamany.KEY_INTERFICIE_SIZE, Guardat.Direccio.Local, 0.8f);

        if (bindables == null)
        {
            bindables = new List<IBindable>(GetComponentsInChildren<IBindable>());
        }

        //MostrarBindings();
        StartCoroutine(MostrarBindingsTemps());

        for (int i = 0; i < bindings.Length; i++)
        {
            bindings[i].GetComponent<RebindActionUI>().startRebindEvent.AddListener(Abans);
            bindings[i].GetComponent<RebindActionUI>().stopRebindEvent.AddListener(Despres);
            bindings[i].GetComponent<RebindActionUI>().guardarEvent.AddListener(AccioDespresDeRebindejar);
            //bindings[i].GetComponent<RebindActionUI>().updateBindingUIEvent.AddListener(Despres);
        }

    }

    private void Despres(RebindActionUI arg0, string arg1, string arg2, string arg3)
    {
        AssignarBindables();
        for (int i = 0; i < bindings.Length; i++)
        {
            bindings[i].Actualitzar();
            bindings[i].MostrarIcone();
        }
    }

    private void Abans(RebindActionUI arg0, InputActionRebindingExtensions.RebindingOperation arg1)
    {
        for (int i = 0; i < bindings.Length; i++)
        {
            bindings[i].RemoveBindables();
        }
    }
    private void Despres(RebindActionUI arg0, InputActionRebindingExtensions.RebindingOperation arg1)
    {
        AssignarBindables();
        for (int i = 0; i < bindings.Length; i++)
        {
            bindings[i].Actualitzar();
            bindings[i].MostrarIcone();
        }
    }

    void AccioDespresDeRebindejar(InputAction inputAction)
    {

        Debug.Log("AccioDespresDeRebindejar");
        input_Bindings.Guardar(inputAction);


        /*for (int i = 0; i < bindings.Length; i++)
        {
            bindings[i].RemoveBindables();
        }
        AssignarBindables();
        for (int i = 0; i < bindings.Length; i++)
        {
            bindings[i].Actualitzar();
            bindings[i].MostrarIcone();
        }*/
    }


    IEnumerator MostrarBindingsTemps()
    {
        yield return new WaitForEndOfFrame();
        MostrarBindings();
    }

    void AssignarBindables()
    {
        for (int bi = 0; bi < bindings.Length; bi++)
        {
            for (int i = 0; i < bindings[bi].IconePerBinding.Icones.Count; i++)
            {
                for (int bl = 0; bl < bindables.Count; bl++)
                {
                    if (bindings[bi].IconePerBinding.Icones[i].path == bindables[bl].Path)
                    {
                        SetBinding(bindings[bi], bindables[bl]);
                    }
                }
            }

        }
    }

    [ContextMenu("MostrarBindings")]
    public void MostrarBindings()
    {
        trobats = new List<UI_Binding>();
        ordenats = new List<UI_Binding>();

        for (int i = 0; i < bindables.Count; i++)
        {
            bindables[i].Activar(false);
        }

        AssignarBindables();

        while (trobats.Count > 0)
        {
            index = 1;
            mesApunt = 0;
            if(trobats.Count != 1) 
            {
                while (index < trobats.Count)
                {
                    if (trobats[index].GetBindable.RectTransform.position.y > trobats[mesApunt].GetBindable.RectTransform.position.y)
                        mesApunt = index;
                    index++;
                }
            }
            ordenats.Add(trobats[mesApunt]);
            trobats.RemoveAt(mesApunt);
        }
        for (int i = 0; i < ordenats.Count; i++)
        {
            if(posicionar)
                ordenats[i].transform.SetParent(ordenats[i].Dreta ? bindingsD : bindingsE);

            ordenats[i].Rebindable = rebindable;
            ordenats[i].Actualitzar();
        }
    }

    void SetBinding(UI_Binding ui, IBindable binding)
    {
        //Debug.Log($"{bindings[b].IconePerBinding.InputBinding.action.name}({bindings[b].IconePerBinding.InputBinding.action.bindings[i].name}) - (){bindables[index].GetPath()}");
        if(!trobats.Contains(ui)) trobats.Add(ui);

        binding.Activar(true);
        //ui.Bindable = binding;
        if (posicionar)
            ui.Dreta = (binding.RectTransform.anchoredPosition.x > 0);

        ui.AddBindable(binding);



    }

    private void OnValidate()
    {
        input_Bindings = XS_Editor.LoadAssetAtPath<Input_Bindings>("Assets/XidoStudio/Inputs/Rebindings/Bindings.asset");
        if(bindings == null || bindings.Length == 0)
        {
            bindings = GetComponentsInChildren<UI_Binding>();
        }

    }
}
