using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tamany : MonoBehaviour
{
    public const string KEY_INTERFICIE_SIZE = "InterficeSize";

    [SerializeField] SavableVariable<float> interfaceSize;
    CanvasScaler canvasScaler;
    float size = 0.8f;
    void OnEnable()
    {
        //interfaceSize = new SavableVariable<float>(guardat, KEY_INTERFICIE_SIZE, true, 0.8f);
        //interfaceSize.Define(guardat, KEY_INTERFICIE_SIZE, true, (float)(guardat.Get(KEY_INTERFICIE_SIZE, 0.8f)));

        SetCanvasScale();
    }

    public void SetCanvasScale()
    {
        //if (canvasScaler == null) canvasScaler = GetComponent<CanvasScaler>();
        size = interfaceSize.Valor;
        canvasScaler.referenceResolution = new Vector2(1920 / size, 1080 / size);
    }

    private void OnValidate() 
    {
        interfaceSize = new SavableVariable<float>(KEY_INTERFICIE_SIZE, true, 0.8f);
        if (canvasScaler == null) canvasScaler = GetComponent<CanvasScaler>();
        if (canvasScaler == null) Debug.LogError("No hi ha un CanvasScaler!!!", this.gameObject);
    } 
}
