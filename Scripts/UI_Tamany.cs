using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tamany : MonoBehaviour
{
    public const string KEY_INTERFICIE_SIZE = "InterficeSize";

    [SerializeField] Guardat guardat;
    [SerializeField] SavableVariable<float> interfaceSize;
    CanvasScaler canvasScaler;
    float size = 1;
    void OnEnable()
    {
        interfaceSize = new SavableVariable<float>(guardat, KEY_INTERFICIE_SIZE, true, (float)(guardat.Get(KEY_INTERFICIE_SIZE, 0.8f)));
        //interfaceSize.Define(guardat, KEY_INTERFICIE_SIZE, true, (float)(guardat.Get(KEY_INTERFICIE_SIZE, 0.8f)));

        SetCanvasScale();
    }

    public void SetCanvasScale()
    {
        if (canvasScaler == null) canvasScaler = GetComponent<CanvasScaler>();

        size = interfaceSize.Valor;
        canvasScaler.referenceResolution = new Vector2(1920 / size, 1080 / size);
    }

    private void OnValidate()
    {
        guardat = XS_Utils.XS_Editor.LoadGuardat<Guardat>();
    }
}
