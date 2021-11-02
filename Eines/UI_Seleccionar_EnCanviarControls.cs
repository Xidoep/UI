using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using XS_Utils;

public class UI_Seleccionar_EnCanviarControls : MonoBehaviour
{
    [SerializeField] Input_ReconeixementTipus[] buscats;
    Selectable selectable;

    bool trobat = false;
    int index = 0;

    void OnEnable()
    {
        if (selectable == null) selectable = GetComponent<Selectable>();
        InputUser.onChange += EnCanviar;
    }

    void OnDisable()
    {
        InputUser.onChange -= EnCanviar;
    }

    void EnCanviar(InputUser inputUser, InputUserChange inputUserChange, InputDevice inputDevice)
    {
        if (inputUserChange == InputUserChange.DevicePaired)
        {
            trobat = false;
            index = 0;
            while (index < buscats.Length && !trobat)
            {
                if (buscats[index].Comparar(PlayerInput.GetPlayerByIndex(0).devices[0]))
                    trobat = true;
                else index++;
            }

            if (trobat) selectable.Select();
        }
    }
}
