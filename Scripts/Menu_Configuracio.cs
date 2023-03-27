using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Menu/Configuracio")]
public class Menu_Configuracio : ScriptableObject
{
    [SerializeField] Localitzacio localitzacio;

    [SerializeField] int idioma;
    [Space(20)]
    [SerializeField] Utils_InstantiableFromProject popupCanviarIdioma;

    public void PopupCanviarIdioma(int idioma) 
    {
        this.idioma = idioma;
        popupCanviarIdioma.InstantiateReturn().GetComponent<Utils_EsdevenimentDelegat>().Registrar(CanviarIdioma);
    }
    void CanviarIdioma() => localitzacio.IdiomaActual(idioma);

    public void ObrirPaguinaFormulariContacteXS() => Application.OpenURL("https://www.xidostudio.com/support");
}
