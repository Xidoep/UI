using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using XS_Utils;

public class UI_Bindings : MonoBehaviour
{
    //[SerializeField] Settings settings;
    SavableVariable<float> interfaceSize;
    //[SerializeField] Guardat guardat;
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

    private void OnEnable()
    {
        
        playerInput = FindObjectOfType<PlayerInput>();

        interfaceSize = new SavableVariable<float>(UI_Tamany.KEY_INTERFICIE_SIZE, Guardat.Direccio.Local, 0.8f);
        if(bindables == null)
        {
            bindables = new List<IBindable>(GetComponentsInChildren<IBindable>());
        }
        //MostrarBindings();
        StartCoroutine(MostrarBindingsTemps());

    }

    IEnumerator MostrarBindingsTemps()
    {
        yield return new WaitForEndOfFrame();
        MostrarBindings();
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

        for (int b = 0; b < bindings.Length; b++)
        {
            for (int i = 0; i < bindings[b].IconePerBinding.InputBinding.action.bindings.Count; i++)
            {
                //if (bindings[b].IconePerBinding.InputBinding.action.bindings[i].isComposite)
                if (bindings[b].IconePerBinding.InputBinding.action.Es2D(playerInput.devices[0], true))
                {
                    
                    for (int c = 0; c < 4; c++)
                    {
                        i++;
                        trobat = false;
                        index = 0;
                        while (index < bindables.Count && !trobat)
                        {
                            if (bindings[b].IconePerBinding.InputBinding.action.bindings[i].CompararPath(bindables[index].GetPath(), true)) trobat = true;
                            else index++;
                        }
                        if (trobat)
                        {
                            //Debug.Log($"{bindings[b].IconePerBinding.InputBinding.action.name}({bindings[b].IconePerBinding.InputBinding.action.bindings[i].name}) - (){bindables[index].GetPath()}");
                            SetBinding(bindings[b], bindables[index]);
                        }
                    }
                }
                else if (bindings[b].IconePerBinding.InputBinding.action.Es1D(playerInput.devices[0], true))
                {
                    for (int c = 0; c < 2; c++)
                    {
                        i++;
                        trobat = false;
                        index = 0;
                        while (index < bindables.Count && !trobat)
                        {
                            if (bindings[b].IconePerBinding.InputBinding.action.bindings[i].CompararPath(bindables[index].GetPath(), true)) trobat = true;
                            else index++;
                        }
                        if (trobat)
                        {
                            //Debug.Log($"{bindings[b].IconePerBinding.InputBinding.action.name}({bindings[b].IconePerBinding.InputBinding.action.bindings[i].name}) - (){bindables[index].GetPath()}");
                            SetBinding(bindings[b], bindables[index]);
                        }
                    }
                }
                else
                {
                    trobat = false;
                    index = 0;
                    while (index < bindables.Count && !trobat)
                    {
                        //Debug.Log(bindings[b].IconePerBinding.InputBinding.action.bindings[i].overridePath + " - " + bindings[b].IconePerBinding.InputBinding.action.bindings[i].path);
                        if (bindings[b].IconePerBinding.InputBinding.action.bindings[i].CompararPath(bindables[index].GetPath(), true)) trobat = true;
                        else index++;
                    }

                    if (trobat)
                    {
                        //Debug.Log($"{bindings[b].IconePerBinding.InputBinding.action.name} - {bindables[index].GetPath()}");
                        SetBinding(bindings[b], bindables[index]);
                    }
                }
                
            }

        }

        
        while(trobats.Count > 0)
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

    /*private void OnValidate()
    {
        guardat = XS_Utils.XS_Editor.LoadGuardat<Guardat>();
    }*/
}
