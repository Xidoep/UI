using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class UI_Bindings : MonoBehaviour
{
    //[SerializeField] Settings settings;
    SavableVariable<float> interfaceSize;
    [SerializeField] Guardat guardat;
    [SerializeField] Transform bindingsE, bindingsD;
    [SerializeField] UI_Binding[] bindings;
    List<IBindable> bindables;

    bool trobat = false;
    int index = 0;
    int mesApunt = 0;

    List<UI_Binding> trobats;
    List<UI_Binding> ordenats;

    private void OnEnable()
    {
        interfaceSize = new SavableVariable<float>();
        interfaceSize.Define(guardat, UI_Tamany.KEY_INTERFICIE_SIZE, true, 1);
        if(bindables == null)
        {
            bindables = new List<IBindable>(GetComponentsInChildren<IBindable>());
        }
    }

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
                if (bindings[b].IconePerBinding.InputBinding.action.bindings[i].isComposite)
                {
                    for (int c = 0; c < 4; c++)
                    {
                        i++;
                        trobat = false;
                        index = 0;
                        while (index < bindables.Count && !trobat)
                        {
                            if (bindings[b].IconePerBinding.InputBinding.action.bindings[i].Comparar(bindables[index].GetPath())) trobat = true;
                            else index++;
                        }
                        if (trobat)
                        {
                            //Debug.Log($"{bindings[b].IconePerBinding.InputBinding.action.name}({bindings[b].IconePerBinding.InputBinding.action.bindings[i].name}) - (){bindables[index].GetPath()}");
                            SetBinding(bindings[b],bindables[index]);
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
                        if (bindings[b].IconePerBinding.InputBinding.action.bindings[i].Comparar(bindables[index].GetPath())) trobat = true;
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
                    if (trobats[index].Bindable.position.y > trobats[mesApunt].Bindable.position.y)
                        mesApunt = index;
                    index++;
                }
            }
            ordenats.Add(trobats[mesApunt]);
            trobats.RemoveAt(mesApunt);
        }
        for (int i = 0; i < ordenats.Count; i++)
        {
            ordenats[i].transform.SetParent(ordenats[i].Dreta ? bindingsD : bindingsE);
            ordenats[i].Actualitzar(interfaceSize.Valor);
        }

    }

    void SetBinding(UI_Binding ui, IBindable binding)
    {
        //Debug.Log($"{bindings[b].IconePerBinding.InputBinding.action.name}({bindings[b].IconePerBinding.InputBinding.action.bindings[i].name}) - (){bindables[index].GetPath()}");
        trobats.Add(ui);

        binding.Activar(true);
        ui.Bindable = binding.Transform;
        ui.Dreta = (binding.Transform.anchoredPosition.x > 0);
        

        //ui.transform.SetParent(ui.Dreta ? bindingsD : bindingsE);
        //ui.SetBinding(binding);

        //ui.Actualitzar();
    }

    private void OnValidate()
    {
        guardat = XS_Utils.XS_Editor.LoadGuardat<Guardat>();
    }
}
