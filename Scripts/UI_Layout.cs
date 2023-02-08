using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Layout : MonoBehaviour
{
    [SerializeField] UI_Bindings uiBindings;
    [SerializeField] UI_TeclatLayout keyboardLayout;
    [SerializeField] Selectable mainButton;

    //AnimacioPerCodi_Enable[] animationsShow;
    //Utils_DisableTempsAnimacio[] animaionsHide;
    bool shown;

    private void OnEnable()
    {
        /*if (animationsShow == null) animationsShow = new AnimacioPerCodi_Enable[0];
        if (animationsShow.Length == 0) animationsShow = GetComponentsInChildren<AnimacioPerCodi_Enable>(true);

        if (animaionsHide == null) animaionsHide = new Utils_DisableTempsAnimacio[0];
        if (animaionsHide.Length == 0) animaionsHide = GetComponentsInChildren<Utils_DisableTempsAnimacio>(true);
 */   }

    public void OnClick()
    {
        /*if (!shown)
        {
            shown = true;
            for (int i = 0; i < animationsShow.Length; i++)
            {
                animationsShow[i].gameObject.SetActive(true);
            }
        }
        else
        {
            shown = false;
            for (int i = 0; i < animaionsHide.Length; i++)
            {
                animaionsHide[i].Disable();
            }
        }*/
    }
    public void OnHover()
    {
        if (!shown)
            return;

        shown = false;
        /*for (int i = 0; i < animaionsHide.Length; i++)
        {
            animaionsHide[i].Disable();
        }*/
    }
    public void SeleccionarTipus(int tipus)
    {
        keyboardLayout.SetLayout(tipus);
        shown = false;
        /*for (int i = 0; i < animaionsHide.Length; i++)
        {
            animaionsHide[i].Disable();
        }*/
        mainButton.Select();
        uiBindings.MostrarBindings();
    }
}
